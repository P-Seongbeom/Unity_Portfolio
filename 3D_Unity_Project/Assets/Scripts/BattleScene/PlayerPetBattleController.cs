using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class PlayerPetBattleController : BattleController, ITarget, IFight
{
    public Vector3 Destination;

    public GameObject SkillRange;

    public ParticleSystem SkillUseEffect;

    protected bool _inPhase = false;

    protected virtual void Awake()
    {
        Controller = GetComponent<PetController>();
        _atk = GetComponent<PetInfo>().ATK;
        _def = GetComponent<PetInfo>().DEF;
        _hp = GetComponent<PetInfo>().HP;
        _skillCost = GetComponent<PetInfo>().SkillCost;
        _attackRange = GetComponent<PetInfo>().AttackRange;
        _skillCooltime = GetComponent<PetInfo>().Cooltime;
        _skillRange = GetComponent<PetInfo>().SkillRange;
        SkillRange = Instantiate(SkillRange);
        SkillRange.SetActive(false);
    }

    protected override void Start()
    {
        if(BattleManager.Instance == null)
        {
            return;
        }
        Destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 250);

        _distanceTarget = (transform.position - Destination).sqrMagnitude;

        Controller.Agent.stoppingDistance = _attackRange;
        
        StartCoroutine(MovePet(Destination));

        base.Start();
    }

    public IEnumerator MovePet(Vector3 destination)
    {
        yield return new WaitForSeconds(2f);
        Controller.SetDestination(destination);
    }

    public void StopPet()
    {
        if(isAlive)
        {
            Controller.SetDestination(this.transform.position);
            _inPhase = true;
        }
    }

    public override IEnumerator ChaseTarget()
    {
        if(SkillMotion)
        {
            yield break;
        }
        if (_inPhase && isAlive)
        {
            foreach (Transform target in BattleManager.Instance.CurrentPhase.transform)
            {
                if(false == Targets.Contains(target.gameObject)
                    && target.gameObject.GetComponent<BattleController>().isAlive)
                {
                    Targets.Add(target.gameObject);
                }
            }

            _distanceTarget = (transform.position - Destination).sqrMagnitude;

            foreach (GameObject target in Targets)
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
                ResetTarget();
            }
        }

        //yield return new WaitForSeconds(0.5f);
        yield return null;

        StartCoroutine(ChaseTarget());
    }

    public override void ResetTarget()
    {
        Targets.Clear();
        Controller.SetDestination(Destination);
        _distanceTarget = (transform.position - Destination).sqrMagnitude;
        _inPhase = false;
    }

    public override void Attack() 
    {
        if(false == _inPhase)
        {
            return;
        }
        _attackDelay += Time.deltaTime;
        _attackReady = _attackRate < _attackDelay;

        if(_attackReady && false == SkillMotion && CurrentTarget.activeSelf 
            && Mathf.Sqrt(_distanceTarget) < _attackRange && Controller.Agent.velocity.z == 0)
        {
            Controller.AttackMotion();
            _attackDelay = 0f;
            if(_longRange)
            {
                RangeAttack();
            }
            else
            {
                StartCoroutine(MeleeAttack());
            }
        }
    }
}

