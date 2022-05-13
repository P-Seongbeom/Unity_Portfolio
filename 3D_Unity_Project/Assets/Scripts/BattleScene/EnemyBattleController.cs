using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene
{
    public class EnemyBattleController : MonoBehaviour, IBattleBehavior
    {
        public PetController Controller;

        public List<Transform> Targets;

        public Vector3 CurrentTargetPosition;

        private float _distanceTarget;

        [SerializeField]
        private float _attackRange;

        private void Start()
        {
            Controller = GetComponent<PetController>();

            for(int i = 0; i < BattleManager.Instance.InBattlePlayerPets.Count; ++i)
            {
                Targets.Add(BattleManager.Instance.InBattlePlayerPets[i].transform);
                _distanceTarget = (transform.position - Targets[i].position).sqrMagnitude;
            }

            Controller.Agent.stoppingDistance = _attackRange;
        }

        private void Update()
        {
            ChaseTarget();
        }

        public void ChaseTarget()
        {
            if(gameObject.activeSelf)
            {
                for(int i = 0; i < BattleManager.Instance.InBattlePlayerPets.Count; ++i )
                {
                    if ((transform.position - BattleManager.Instance.InBattlePlayerPets[i].transform.position).sqrMagnitude < _distanceTarget)
                    {
                        CurrentTargetPosition = BattleManager.Instance.InBattlePlayerPets[i].transform.position;
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
