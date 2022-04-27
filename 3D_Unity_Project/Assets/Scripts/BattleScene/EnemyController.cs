using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene
{
    public class EnemyController : MonoBehaviour, IBattleBehavior
    {
        public PlayerPetController Controller;

        public List<Transform> Targets;

        public Vector3 CurrentTargetPosition;

        private float _distanceTarget;

        private void Start()
        {
            Controller = GetComponent<PlayerPetController>();

            foreach (Transform target in BattleManager.Instance.PlayerPets.transform)
            {
                Targets.Add(target.GetComponent<Transform>());
                _distanceTarget = (transform.position - target.position).sqrMagnitude;
            }
        }

        private void Update()
        {
            ChaseTarget();
        }

        public void ChaseTarget()
        {
            if(gameObject.activeSelf)
            {
                foreach (Transform target in BattleManager.Instance.PlayerPets.transform)
                {
                    if ((transform.position - target.position).sqrMagnitude < _distanceTarget)
                    {
                        //Enemy = target;
                        CurrentTargetPosition = target.position;
                        _distanceTarget = (transform.position - CurrentTargetPosition).sqrMagnitude;
                    }
                }
            }

            Controller.SetDestination(CurrentTargetPosition);
        }

        public void ResetTarget()
        { 
        }

    }
}
