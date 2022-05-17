using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : PlayerPetBattleController
{
    protected override void Update()
    {
        if (BattleManager.Instance == null)
        {
            return;
        }
        base.Update();
        UseSkill();
    }

    public override void UseSkill()
    {
        if (false == BattleUI.Instance._waitSkillUse)
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 rangePos = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
            SkillRange.transform.position = rangePos;
            //SkillRange.transform.right = 
            //SkillRange.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            SkillRange.SetActive(false);

            _usingSkill = true;

            Time.timeScale = 1f;

            BattleUI.Instance.CostUpdate(_skillCost, _skillCooltime);

            Controller.SkillMotion();
            //StartCoroutine(Provoke());
        }
    }

    IEnumerator Rush()
    {
        Controller.Agent.isStopped = true;
        Controller.Agent.stoppingDistance = 0f;
        //Controller.Agent.SetDestination()
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_usingSkill && gameObject.tag != other.tag)
        {
            other.GetComponent<BattleController>().Damaged(_atk);
        }
    }
}
