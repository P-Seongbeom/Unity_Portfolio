using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BattleScene
{
    public class PlayerPetBattleController : MonoBehaviour, IBattleBehavior
    {
        public PetController Controller;

        public Vector3 Destination;

        public List<Transform> Targets;
        public Vector3 CurrentTargetPosition;

        [SerializeField]
        private float _attackRange;

        private float _distanceTarget;
        private bool _inPhase = false;

        private void Start()
        {
            Controller = GetComponent<PetController>();

            Destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 250);

            CurrentTargetPosition = Destination;

            _distanceTarget = (transform.position - CurrentTargetPosition).sqrMagnitude;

            Controller.Agent.stoppingDistance = _attackRange;

            StartCoroutine(MovePet(Destination));
        }

        private void Update()
        {
            ChaseTarget();
        }

        public IEnumerator MovePet(Vector3 destination)
        {
            yield return new WaitForSeconds(2f);
            Controller.SetDestination(destination);
        }

        public void StopPet()
        {
            Controller.SetDestination(this.transform.position);
            _inPhase = true;
        }

        public void ChaseTarget()
        {
            if(_inPhase)
            {
                foreach (Transform target in BattleManager.Instance.CurrentPhase.transform)
                {
                    if(false == Targets.Contains(target))
                    {
                        Targets.Add(target.GetComponent<Transform>());
                    }

                    if((transform.position - target.position).sqrMagnitude < _distanceTarget)
                    {
                        CurrentTargetPosition = target.position;
                        _distanceTarget = (transform.position - CurrentTargetPosition).sqrMagnitude;
                    }
                }

                Controller.SetDestination(CurrentTargetPosition);
            }
        }

        public void ResetTarget()
        {
            Targets.Clear();
            CurrentTargetPosition = Destination;
            _distanceTarget = (transform.position - CurrentTargetPosition).sqrMagnitude;
            _inPhase = false;
        }

        public void Attack()
        {

        }

        public void UseSkill()
        {

        }

        public void Die()
        {

        }
    }
}
