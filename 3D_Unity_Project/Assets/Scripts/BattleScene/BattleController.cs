using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleController : MonoBehaviour, ITarget, IFight
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
    public float _skillRange;
    public bool SkillReady = true;
    public bool UsingSkill = false;
    public bool SkillMotion = false;
    public int _skillCost;

    protected float _distanceTarget;

    public bool isAlive = true;
    public bool _longRange;

    protected virtual void Start()
    {
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

    protected virtual void Update()
    {
        if (BattleManager.Instance == null)
        {
            return;
        }

        if (isAlive)
        {
            Attack();
        }
    }

    public abstract IEnumerator ChaseTarget();
    public abstract void ResetTarget();

    public abstract void Attack();

    public virtual void RangeAttack()
    {
        GameObject bullet = Instantiate(Bullet, BulletPos.position, BulletPos.rotation);

        float height = bullet.GetComponent<Bullet>().Height;
        Vector3 vec = CurrentTarget.transform.position - bullet.transform.position;
        vec.y = Vector3.Lerp(new Vector3(CurrentTarget.transform.position.x, CurrentTarget.transform.position.y + height, CurrentTarget.transform.position.z),
                             bullet.transform.position, 0.5f).y;

        bullet.GetComponent<Bullet>().Target = CurrentTarget;

        Rigidbody bulletRigid = bullet.GetComponent<Rigidbody>();
        bulletRigid.AddForce(vec, ForceMode.Impulse);
    }

    public virtual IEnumerator MeleeAttack()
    {
        yield return new WaitForSeconds(0.5f);
        CurrentTarget.GetComponent<BattleController>().Damaged(_atk);
    }

    public virtual void Damaged(int damage)
    {
        int totalDamgae = (int)(damage - _def * 0.5f);

        if (totalDamgae < 1)
        {
            _hp -= 1;
        }
        else
        {
            _hp -= totalDamgae;
        }
        //print($"플레이어 : {_hp}");

        if (_hp < 1)
        {
            isAlive = false;
            Die();
        }
    }

    public virtual void Die()
    {
        StartCoroutine(Disappear());
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        Controller.Agent.enabled = false;
        Controller.DieMotion();
    }

    public virtual IEnumerator Disappear()
    {
        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);
    }

    public abstract void UseSkill();
}
