using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BattleScene
{
    public class PetMover : MonoBehaviour, IBattleBehavior
    {
        public PlayerPetController Controller;

        public Vector3 Destination;

        private void Start()
        {
            Controller = GetComponent<PlayerPetController>();
            //юс╫ц
            Destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 250);
            StartCoroutine(MovePet(Destination));
        }

        private void Update()
        {
        }

        public IEnumerator MovePet(Vector3 destination)
        {
            yield return new WaitForSeconds(2f);
            CameraController.Instance.MoveCamera = true;
            Controller.SetDestination(destination);
        }

        public void StopPet()
        {
            Controller.SetDestination(this.transform.position);
        }

        public void SetTarget()
        {
            
        }
    }
}
