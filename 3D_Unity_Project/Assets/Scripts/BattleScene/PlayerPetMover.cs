using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BattleScene
{
    public class PlayerPetMover : MonoBehaviour, IBattleBehavior
    {
        public PlayerPetController Controller;

        public Vector3 Destination;

        public List<Transform> Targets;
        public Vector3 CurrentTargetPosition;

        public Transform Enemy;
        [SerializeField]
        private float AttackRange;

        private float _distanceTarget;

        private void Start()
        {
            Controller = GetComponent<PlayerPetController>();
            Destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 250);
            CurrentTargetPosition = Destination;
            _distanceTarget = (transform.position - CurrentTargetPosition).sqrMagnitude;
            StartCoroutine(MovePet(Destination));
        }

        private void Update()
        {
        }

        public IEnumerator MovePet(Vector3 destination)
        {
            yield return new WaitForSeconds(2f);
            Controller.SetDestination(destination);
        }

        public void StopPet()
        {
            Controller.SetDestination(this.transform.position);
        }

        public void SetTarget()
        {
            foreach(Transform target in BattleManager.Instance.CurrentPhase.transform)
            {
                Targets.Add(target.GetComponent<Transform>());

                if((transform.position - target.position).sqrMagnitude < _distanceTarget)
                {
                    Enemy = target;
                    CurrentTargetPosition = target.position;
                    _distanceTarget = (transform.position - CurrentTargetPosition).sqrMagnitude;
                }
            }

            Controller.SetDestination(CurrentTargetPosition);
        }

        public void ResetTarget()
        {
            Targets.Clear();
            CurrentTargetPosition = Destination;
            _distanceTarget = (transform.position - CurrentTargetPosition).sqrMagnitude;
        }


    }
}
