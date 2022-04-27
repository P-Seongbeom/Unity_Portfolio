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

        public GameObject Pets;
        public List<PlayerPetMover> PetMovers;

        public List<GameObject> Phases;
        public GameObject CurrentPhase;
        private int _phaseCount = 0;

        public bool _isBattle = false;

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
            PetMovers.AddRange(Pets.GetComponentsInChildren<PlayerPetMover>());
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
                    _isBattle = true;

                    foreach(PlayerPetMover pet in PetMovers)
                    {
                        pet.StopPet();
                        pet.SetTarget();
                    }
                }
            }
        }


        public void SetCurrentPhase(int phaseCount)
        {
            CurrentPhase = Phases[phaseCount];
        }

        public void EndPhase()
        {
            _isBattle = false;
            foreach (PlayerPetMover pet in PetMovers)
            {
                pet.ResetTarget();
                StartCoroutine(pet.MovePet(pet.Destination));
            }
        }
    }
}
