using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BattleScene
{
    public class PetMover : MonoBehaviour
    {
        //[SerializeField]
        //private float _moveSpeed = 10f;

        //public GameObject Pet;
        public PlayerPetController Controller;
        public Vector3 Destination;

        private void Start()
        {
            //Pet = this.gameObject;
            Controller = GetComponent<PlayerPetController>();
            //юс╫ц
            //Destination = new Vector3(Pet.transform.position.x, Pet.transform.position.y, Pet.transform.position.z + 250);
            Destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 250);
            StartCoroutine(MovePet(Destination));
        }

        private void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                StartCoroutine(MovePet(Destination));
            }
        }

        public IEnumerator MovePet(Vector3 destination)
        {
            yield return new WaitForSeconds(2f);
            CameraFocus.Instance.MoveCamera = true;
            Controller.SetDestination(destination);
        }


    }
}
