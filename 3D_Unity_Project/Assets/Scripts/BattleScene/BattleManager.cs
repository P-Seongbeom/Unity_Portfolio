using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public List<GameObject> InStageEnemy;

    public List<GameObject> Phases;
    public GameObject CurrentPhase;
    public bool inBattle;
    public bool inPhase;
    public bool isClear;

    public float OverallCost;

    public string FarmSceneName;
    [SerializeField]
    private float _costUpPerSecond;

    private int _phaseCount = 0;
    private int _StageFailCount;
    private int _StageClearCount;

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
            for(int i = 0; i < phase.transform.childCount; ++i)
            {
                InStageEnemy.Add(phase.transform.GetChild(i).gameObject);
            }
        }

        foreach (GameObject phase in Phases)
        {
            phase.SetActive(false);
        }

        inBattle = true;
    }

    void Update()
    {
        CostUp();

        ActivateCheckPoint();

        if (Input.GetKeyDown("space"))
        {
            EndPhase();
        }

        CheckClear();
        CheckFail();
    }

    public void ActivateCheckPoint()
    {
        foreach(Transform point in CheckPoints)
        {
            if(point.gameObject.activeSelf && CameraController.Instance.transform.position.z >= point.position.z)
            {
                point.gameObject.SetActive(false);

                SetCurrentPhase(_phaseCount);
                ++_phaseCount;

                foreach(PlayerPetBattleController pet in PetMovers)
                {
                    pet.StopPet();
                }
            }
        }
    }
    public void CheckClear()
    {
        if (CurrentPhase == null || inBattle == false)
        {
            return;
        }

        int clearcount = 0;

        if (CurrentPhase == Phases[Phases.Count - 1])
        {
            for (int i = 0; i < CurrentPhase.transform.childCount; ++i)
            {
                if (false == CurrentPhase.transform.GetChild(i).gameObject.activeSelf)
                {
                    ++clearcount;
                }
                _StageClearCount = clearcount;
            }
        }

        if (_StageClearCount == CurrentPhase.transform.childCount)
        {
            StartCoroutine(StageClear());
            inBattle = false;
            isClear = true;
        }
    }

    public void CheckFail()
    {
        if(CurrentPhase == null || inBattle == false)
        {
            return;
        }

        int failcount = 0;

        foreach (GameObject pet in InBattlePlayerPets)
        {

            if (false == pet.activeSelf)
            {
                ++failcount;
            }
            _StageFailCount = failcount;
        }

        if(_StageFailCount == InBattlePlayerPets.Count)
        {
            StartCoroutine(StageFail());
            inBattle = false;
            isClear = false;
        }
    }

    public IEnumerator StageFail()
    {
        foreach(GameObject hpbar in BattleUI.Instance.EnemyHpBarList)
        {
            hpbar.SetActive(false);
        }

        yield return new WaitForSeconds(2);

        BattleUI.Instance.StageOverUI.SetActive(true);

        yield return new WaitForSeconds(2);

        BattleUI.Instance.FailPhrase();
    }

    public IEnumerator StageClear()
    {
        foreach (PlayerPetBattleController pet in PetMovers)
        {
            pet.StopPet();
        }

        yield return new WaitForSeconds(2);

        BattleUI.Instance.StageOverUI.SetActive(true);

        yield return new WaitForSeconds(2);

        if(isClear)
        {
            BattleUI.Instance.ClearPhrase();
        }
        else
        {
            BattleUI.Instance.FailPhrase();
        }
    }


    public void SetCurrentPhase(int phaseCount)
    {
        inPhase = true;
        CurrentPhase = Phases[phaseCount];
        CurrentPhase.SetActive(true);
    }

    public void EndPhase()
    {
        inPhase = false;
        CurrentPhase.SetActive(false);

        foreach (PlayerPetBattleController pet in PetMovers)
        {
            pet.ResetTarget();
            StartCoroutine(pet.MovePet(pet.Destination));
        }
    }

    void CostUp()
    {
        if(OverallCost < 10)
        {
            OverallCost += _costUpPerSecond * Time.deltaTime;
        }
    }

    public void BackToFarm()
    {
        SceneManager.LoadScene(FarmSceneName);
    }
}
