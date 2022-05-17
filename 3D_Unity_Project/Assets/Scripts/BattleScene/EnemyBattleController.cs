using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBattleController : BattleController, ITarget, IFight
{
    public bool provocated = false;

    public Vector3 PlayerStartPoint;

    protected virtual void Awake()
    {
        Controller = GetComponent<PetController>();

        _atk = GetComponent<EPetInfo>().ATK;
        _def = GetComponent<EPetInfo>().DEF;
        _hp = GetComponent<EPetInfo>().HP;
        _attackRange = GetComponent<EPetInfo>().AttackRange;
        _skillCooltime = GetComponent<EPetInfo>().Cooltime;
        _skillRange = GetComponent<EPetInfo>().SkillRange;
        _skillCost = 0;
    }

    protected override void Start()
    {
        if (BattleManager.Instance == null)
        {
            return;
        }
        for(int i = 0; i < BattleManager.Instance.InBattlePlayerPets.Count; ++i)
        {
            Targets.Add(BattleManager.Instance.InBattlePlayerPets[i]);
        }

        _distanceTarget = (transform.position - Targets[0].transform.position).sqrMagnitude;
        PlayerStartPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z - 250);

        Controller.Agent.stoppingDistance = _attackRange;

        base.Start();
    }

    public override IEnumerator ChaseTarget()
    {
        if(isAlive && false == provocated)
        {
            for(int i = 0; i < BattleManager.Instance.InBattlePlayerPets.Count; ++i )
            {
                if(false == Targets.Contains(BattleManager.Instance.InBattlePlayerPets[i])
                    && BattleManager.Instance.InBattlePlayerPets[i].GetComponent<BattleController>().isAlive)
                {
                    Targets.Add(BattleManager.Instance.InBattlePlayerPets[i]);
                }
            }

            _distanceTarget = (transform.position - PlayerStartPoint).sqrMagnitude;

            foreach(GameObject target in Targets)
            {
                if((transform.position - target.transform.position).sqrMagnitude < _distanceTarget
                    && target.gameObject.GetComponent<BattleController>().isAlive)
                {
                    CurrentTarget = target;
                    _distanceTarget = (transform.position - CurrentTarget.transform.position).sqrMagnitude;
                }
            }
            transform.forward = (CurrentTarget.transform.position - transform.position).normalized;
            Controller.SetDestination(CurrentTarget.transform.position);

            if(false == CurrentTarget.GetComponent<BattleController>().isAlive)
            {
                StopPet();
            }

        }
        else if(isAlive && provocated)
        {
            if(CurrentTarget.GetComponent<BattleController>().isAlive)
            {
                Controller.SetDestination(CurrentTarget.transform.position);
                _distanceTarget = (transform.position - CurrentTarget.transform.position).sqrMagnitude;
            }
            else
            {
                provocated = false;
            }
        }

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(ChaseTarget());
    }

    public override void ResetTarget()
    {

    }

    public override void Attack()
    {
        _attackDelay += Time.deltaTime;
        _attackReady = _attackRate < _attackDelay;

        if (_attackReady && false == SkillMotion && CurrentTarget.activeSelf
            && Mathf.Sqrt(_distanceTarget) < _attackRange && Controller.Agent.velocity.z == 0)
        {
            Controller.AttackMotion();
            _attackDelay = 0f;
            if (_longRange)
            {
                RangeAttack();
            }
            else
            {
                StartCoroutine(MeleeAttack());
            }
        }
    }

    public void StopPet()
    {
        if (isAlive)
        {
            Controller.SetDestination(this.transform.position);
        }
    }
}
