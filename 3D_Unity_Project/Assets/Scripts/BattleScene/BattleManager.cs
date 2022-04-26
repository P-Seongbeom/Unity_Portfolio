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
        public List<PetMover> PetMovers;

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
            PetMovers.AddRange(Pets.GetComponentsInChildren<PetMover>());
        }

        void Update()
        {
            ActivateCheckPoint();

            if (Input.GetKeyDown("space"))
            {
                _isBattle = false;
                ResumeMovement();
            }

            //ResumeMovement();
        }

        public void ActivateCheckPoint()
        {
            foreach(Transform point in CheckPoints)
            {
                if(point.gameObject.activeSelf && CameraController.Instance.transform.position.z >= point.position.z)
                {
                    CameraController.Instance.MoveCamera = false;
                    point.gameObject.SetActive(false);

                    Debug.Log($"∆‰¿Ã¡Ó : {_phaseCount + 1}");
                    SetCurrentPhase(_phaseCount);
                    ++_phaseCount;
                    _isBattle = true;

                    foreach(PetMover pet in PetMovers)
                    {
                        pet.StopPet();
                        pet.SetTarget();
                    }
                }
            }
        }

        public void ResumeMovement()
        {
            if(false == _isBattle)
            {
                //Debug.Log("???");
                foreach (PetMover pet in PetMovers)
                {
                    StartCoroutine(pet.MovePet(pet.Destination));
                }
            }
        }

        public void SetCurrentPhase(int phaseCount)
        {
            CurrentPhase = Phases[phaseCount];
        }
    }
}
