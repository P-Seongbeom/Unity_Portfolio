using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class PlayerPetBattleController : MonoBehaviour, ITarget, IFight
{
    public PetController Controller;

    public Vector3 Destination;

    public List<GameObject> Targets;
    public GameObject CurrentTarget;
    public Transform BulletPos;
    public GameObject Bullet;

    public TrailRenderer AttackEffect;
    public TrailRenderer SkillEffect;

    protected int _hp;
    protected int _atk;
    protected int _def;
    protected float _attackRate = 1f;
    protected float _attackDelay;
    protected float _attackRange;
    protected bool _attackReady;
    protected float _skillCooltime;
    protected float _skillDelay;
    protected float _skillRange;
    protected bool _skillReady;
    protected bool _usingSkill;
    protected int _skillCost;

    protected float _distanceTarget;
    protected bool _inPhase = false;
    public bool isAlive = true;
    public bool _longRange;

    private void Awake()
    {
        Controller = GetComponent<PetController>();
        _atk = GetComponent<PetInfo>().ATK;
        _def = GetComponent<PetInfo>().DEF;
        _hp = GetComponent<PetInfo>().HP;
        _skillCost = GetComponent<PetInfo>().SkillCost;
        _attackRange = GetComponent<PetInfo>().AttackRange;
        _skillCooltime = GetComponent<PetInfo>().Cooltime;
        _skillRange = GetComponent<PetInfo>().SkillRange;
    }

    private void Start()
    {
        if(BattleManager.Instance == null)
        {
            return;
        }
        Destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 250);

        _distanceTarget = (transform.position - Destination).sqrMagnitude;

        Controller.Agent.stoppingDistance = _attackRange;
        
        StartCoroutine(MovePet(Destination));

        if (_attackRange < 10)
        {
            _longRange = false;
        }
        else
        {
            _longRange = true;
        }

        StartCoroutine(ChaseTarget());
    }

    private void Update()
    {
        if(BattleManager.Instance == null)
        {
            return;
        }

        if(isAlive)
        {
            Attack();
        }
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

    public IEnumerator ChaseTarget()
    {
        if (_inPhase && isAlive)
        {
            foreach (Transform target in BattleManager.Instance.CurrentPhase.transform)
            {
                if(false == Targets.Contains(target.gameObject)
                    && target.gameObject.GetComponent<EnemyBattleController>().isAlive)
                {
                    Targets.Add(target.gameObject);
                }
            }

            _distanceTarget = (transform.position - Destination).sqrMagnitude;

            foreach (GameObject target in Targets)
            {
                if((transform.position - target.transform.position).sqrMagnitude < _distanceTarget
                    && target.gameObject.GetComponent<EnemyBattleController>().isAlive)
                {
                    CurrentTarget = target;
                    _distanceTarget = (transform.position - CurrentTarget.transform.position).sqrMagnitude;
                }
            }
            transform.forward = (CurrentTarget.transform.position - transform.position).normalized;
            Controller.SetDestination(CurrentTarget.transform.position);

            if(false == CurrentTarget.GetComponent<EnemyBattleController>().isAlive)
            {
                ResetTarget();
            }
        }

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(ChaseTarget());
    }

    public void ResetTarget()
    {
        Targets.Clear();
        Controller.SetDestination(Destination);
        _distanceTarget = (transform.position - Destination).sqrMagnitude;
        _inPhase = false;
    }

    public void Attack() 
    {
        if(false == _inPhase)
        {
            return;
        }
        _attackDelay += Time.deltaTime;
        _attackReady = _attackRate < _attackDelay;

        if(_attackReady && false == _usingSkill && CurrentTarget.activeSelf 
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
                CurrentTarget.GetComponent<EnemyBattleController>().Damaged(_atk);
            }
        }
    }

    public void RangeAttack()
    {
        GameObject bullet = Instantiate(Bullet, BulletPos.position, BulletPos.rotation);
        Vector3 vec = CurrentTarget.transform.position - bullet.transform.position;
        float height = bullet.GetComponent<Bullet>().Height;
        vec.y = Vector3.Lerp(new Vector3(CurrentTarget.transform.position.x, CurrentTarget.transform.position.y + height, CurrentTarget.transform.position.z),
                             bullet.transform.position, 0.5f).y;

        Rigidbody bulletRigid = bullet.GetComponent<Rigidbody>();
        bulletRigid.AddForce(vec, ForceMode.Impulse);
    }

    public void Damaged(int damage)
    {
        //_hp -= (damage - _def);
        //print($"플레이어 : {_hp}");

        //if (_hp < 0)
        //{
        //    isAlive = false;
        //    Die();
        //}
    }

    public void Die()
    {
        StartCoroutine(Disappear());
        Controller.DieMotion();
    }

    public IEnumerator Disappear()
    {
        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);
    }

    public abstract void UseSkill();
}

