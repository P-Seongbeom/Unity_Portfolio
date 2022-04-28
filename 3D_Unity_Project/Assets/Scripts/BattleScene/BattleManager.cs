using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance;

        public List<GameObject> Stages;

        public List<Transform> CheckPoints;

        public GameObject PlayerPets;
        public List<PlayerPetBattleController> PetMovers;

        public List<GameObject> Phases;
        public GameObject CurrentPhase;
        private int _phaseCount = 0;

        //public bool _isBattle = false;

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
        }

        void Start()
        {
            PetMovers.AddRange(PlayerPets.GetComponentsInChildren<PlayerPetBattleController>());

            foreach(GameObject phase in Phases)
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
                    //_isBattle = true;

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
            //_isBattle = false;
            CurrentPhase.SetActive(false);
            foreach (PlayerPetBattleController pet in PetMovers)
            {
                pet.ResetTarget();
                StartCoroutine(pet.MovePet(pet.Destination));
            }
        }
    }
}
