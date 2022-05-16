using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpaca : PlayerPetBattleController
{
    public override void UseSkill()
    {
        _usingSkill = true;
        StartCoroutine(Provoke());
    }

    IEnumerator Provoke()
    {
        Controller.SkillMotion();

        for (int i = 0; i < Targets.Count; ++i)
        {
            if (Vector3.Distance(transform.position, Targets[i].transform.position) < _skillRange)
            {
                Targets[i].GetComponent<EnemyBattleController>().provocated = true;
                Targets[i].GetComponent<EnemyBattleController>().CurrentTarget = gameObject;
            }
        }

        yield return new WaitForSeconds(5f);

        for (int i = 0; i < Targets.Count; ++i)
        {
            if (Vector3.Distance(transform.position, Targets[i].transform.position) < _skillRange)
            {
                Targets[i].GetComponent<EnemyBattleController>().provocated = false;
            }
        }
    }
}
