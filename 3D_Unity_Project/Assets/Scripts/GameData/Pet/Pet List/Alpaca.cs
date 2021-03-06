using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpaca : PlayerPetBattleController
{
    protected override void Update()
    {
        if(BattleManager.Instance == null)
        {
            return;
        }
        base.Update();
        UseSkill();
    }

    public override void UseSkill()
    {
        if(false == UsingSkill)
        {
            return;
        }

        if (BattleUI.Instance.ClickDown)
        {
            SkillRange.SetActive(true);
        }
        else
        {
            UsingSkill = false;
            SkillRange.SetActive(false);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 rangePos = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
            SkillRange.transform.position = rangePos;
        }

        if(Input.GetMouseButtonUp(0) && BattleUI.Instance.ClickDown)
        {
            UsingSkill = false;

            SkillMotion = true;

            Time.timeScale = 1f;

            BattleUI.Instance.CostUpdate(_skillCost, _skillCooltime);

            SkillRange.SetActive(false);

            StopCoroutine(ChaseTarget());
            
            StartCoroutine(Provoke());
        }
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
