using System.Collections;

public interface ITarget
{
    IEnumerator ChaseTarget();
    void ResetTarget();
}

public interface IFight
{
    void Attack();
    void RangeAttack();
    void UseSkill();
    void Damaged(int dmg);
    void Die();
}
