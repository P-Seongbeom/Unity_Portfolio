using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleScene;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public List<GameObject> Stages;
    public GameObject CurrentStage;

    public List<Transform> AgentPetPosition;
    public List<Transform> CheckPoints;

    public List<GameObject> InBattlePlayerPets;
    public List<PlayerPetBattleController> PetMovers;

    public List<GameObject> Phases;
    public GameObject CurrentPhase;
    private int _phaseCount = 0;

    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        for (int i = 0; i < AgentPetPosition.Count; ++i)
        {
            InBattlePlayerPets.Add(Instantiate(GameManager.Instance.AgentPets[i]));
            InBattlePlayerPets[i].transform.position = AgentPetPosition[i].position;
            PetMovers.Add(InBattlePlayerPets[i].GetComponent<PlayerPetBattleController>());
        }

        CurrentStage = Instantiate(Stages[0]);

        foreach (Transform point in CurrentStage.transform.GetChild(1).transform)
        {
            CheckPoints.Add(point);
        }
        foreach (Transform phase in CurrentStage.transform.GetChild(2).transform)
        {
            Phases.Add(phase.gameObject);
        }

        foreach (GameObject phase in Phases)
        {
            phase.SetActive(false);
        }
    }

    void Update()
    {
        ActivateCheckPoint();

        if (Input.GetKeyDown("space"))
        {
            EndPhase();
        }
    }

    public void ActivateCheckPoint()
    {
        foreach(Transform point in CheckPoints)
        {
            if(point.gameObject.activeSelf && CameraController.Instance.transform.position.z >= point.position.z)
            {
                point.gameObject.SetActive(false);

                Debug.Log($"������ : {_phaseCount + 1}");
                SetCurrentPhase(_phaseCount);
                ++_phaseCount;

                foreach(PlayerPetBattleController pet in PetMovers)
                {
                    pet.StopPet();
                    pet.ChaseTarget();
                }
            }
        }
    }


    public void SetCurrentPhase(int phaseCount)
    {
        CurrentPhase = Phases[phaseCount];
        CurrentPhase.SetActive(true);
    }

    public void EndPhase()
    {
        CurrentPhase.SetActive(false);
        foreach (PlayerPetBattleController pet in PetMovers)
        {
            pet.ResetTarget();
            StartCoroutine(pet.MovePet(pet.Destination));
        }
    }
}
