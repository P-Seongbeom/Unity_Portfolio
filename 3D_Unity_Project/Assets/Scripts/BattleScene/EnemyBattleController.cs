using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBattleController : MonoBehaviour, ITarget, IFight
{
    public PetController Controller;

    public List<GameObject> Targets;

    public GameObject CurrentTarget;

    public Transform BulletPos;
    public GameObject Bullet;

    public int _hp;
    public int _atk;
    public int _def;
    public float _attackRate = 1f;
    public float _attackDelay;
    public float _attackRange;
    public bool _attackReady;
    public float _skillCooltime;
    public float _skillDelay;
    public float _skillRange;
    public bool _skillReady;
    public bool _usingSkill;

    protected float _distanceTarget;

    public bool isAlive = true;
    public bool _longRange;

    public bool provocated = false;

    public Vector3 PlayerStartPoint;

    private void Awake()
    {
        Controller = GetComponent<PetController>();

        _atk = GetComponent<EPetInfo>().ATK;
        _def = GetComponent<EPetInfo>().DEF;
        _hp = GetComponent<EPetInfo>().HP;
        _attackRange = GetComponent<EPetInfo>().AttackRange;
        _skillCooltime = GetComponent<EPetInfo>().Cooltime;
        _skillRange = GetComponent<EPetInfo>().SkillRange;
    }

    private void Start()
    {
        if(BattleManager.Instance == null)
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
        if (BattleManager.Instance == null)
        {
            return;
        }

        if(isAlive)
        {
            Attack();
        }
    }

    public IEnumerator ChaseTarget()
    {
        if(isAlive && false == provocated)
        {
            for(int i = 0; i < BattleManager.Instance.InBattlePlayerPets.Count; ++i )
            {
                if(false == Targets.Contains(BattleManager.Instance.InBattlePlayerPets[i])
                    && BattleManager.Instance.InBattlePlayerPets[i].GetComponent<PlayerPetBattleController>().isAlive)
                {
                    Targets.Add(BattleManager.Instance.InBattlePlayerPets[i]);
                }
            }

            _distanceTarget = (transform.position - PlayerStartPoint).sqrMagnitude;

            foreach(GameObject target in Targets)
            {
                if((transform.position - target.transform.position).sqrMagnitude < _distanceTarget
                    && target.gameObject.GetComponent<PlayerPetBattleController>().isAlive)
                {
                    CurrentTarget = target;
                    _distanceTarget = (transform.position - CurrentTarget.transform.position).sqrMagnitude;
                }
            }
            transform.forward = (CurrentTarget.transform.position - transform.position).normalized;
            Controller.SetDestination(CurrentTarget.transform.position);

            if(false == CurrentTarget.GetComponent<PlayerPetBattleController>().isAlive)
            {
                StopPet();
            }

        }
        else if(isAlive && provocated)
        {
            if(CurrentTarget.GetComponent<PlayerPetBattleController>().isAlive)
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

    public void ResetTarget()
    {

    }

    public void Attack()
    {
        _attackDelay += Time.deltaTime;
        _attackReady = _attackRate < _attackDelay;

        if (_attackReady && false == _usingSkill && CurrentTarget.activeSelf
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
                CurrentTarget.GetComponent<PlayerPetBattleController>().Damaged(_atk);
            }
        }
    }

    public void RangeAttack()
    {
        GameObject bullet = Instantiate(Bullet, BulletPos.position, BulletPos.rotation);

        float height = bullet.GetComponent<Bullet>().Height;
        Vector3 vec = CurrentTarget.transform.position - bullet.transform.position;
        vec.y = Vector3.Lerp(new Vector3(CurrentTarget.transform.position.x, CurrentTarget.transform.position.y + height, CurrentTarget.transform.position.z),
                             bullet.transform.position, 0.5f).y;

        bullet.GetComponent<Bullet>().myTag = gameObject.tag;

        Rigidbody bulletRigid = bullet.GetComponent<Rigidbody>();
        bulletRigid.AddForce(vec, ForceMode.Impulse);
    }


    public void Damaged(int damage)
    {
        int totalDamgae = damage - _def;

        if(totalDamgae < 1)
        {
            _hp -= 1;
        }
        else
        {
            _hp -= totalDamgae;
        }
        //print($"Àû : {_hp}");

        if (_hp < 1)
        {
            Die();
            isAlive = false;
        }
    }

    public void Die()
    {
        StartCoroutine(Disappear());
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        Controller.Agent.enabled = false;
        Controller.DieMotion();
    }

    public IEnumerator Disappear()
    {
        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);
    }

    public abstract void UseSkill();

    public void StopPet()
    {
        if (isAlive)
        {
            Controller.SetDestination(this.transform.position);
        }
    }
}
