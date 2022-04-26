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
        public PetMover[] PetMovers;

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
            PetMovers = Pets.GetComponentsInChildren<PetMover>();
        }

        void Update()
        {
            ActivateCheckPoint();

            if (Input.GetKeyDown("space"))
            {
                _isBattle = false;
            }

            ResumeMovement();
        }

        public void ActivateCheckPoint()
        {
            foreach(Transform point in CheckPoints)
            {
                if(point.gameObject.activeSelf && CameraFocus.Instance.transform.position.z >= point.position.z)
                {
                    CameraFocus.Instance.MoveCamera = false;
                    point.gameObject.SetActive(false);

                    foreach(PetMover pet in PetMovers)
                    {
                        _isBattle = true;
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
                foreach (PetMover pet in PetMovers)
                {
                    StartCoroutine(pet.MovePet(pet.Destination));
                }
            }
        }
    }
}
