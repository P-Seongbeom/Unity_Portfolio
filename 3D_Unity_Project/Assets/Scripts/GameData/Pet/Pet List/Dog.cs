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
    //private void OnDrawGizmos()
    //{
    //    Debug.DrawRay(transform.position, transform.forward * 15f, Color.red);
    //    Debug.DrawRay(SkillRange.transform.position, SkillRange.transform.forward * 15f, Color.green);
    //}

    public override void UseSkill()
    {
        if (false == (BattleUI.Instance._waitSkillUse && UsingSkill))
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 rangePos = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
            SkillRange.transform.position = rangePos;
            SkillRange.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            SkillMotion = true;

            SkillRange.SetActive(false);

            Time.timeScale = 1f;

            BattleUI.Instance.CostUpdate(_skillCost, _skillCooltime);

            StartCoroutine(Rush());
        }
    }

    IEnumerator Rush()
    {
        Controller.SkillMotion();

        yield return new WaitForSeconds(1f);

        if(false == CurrentTarget.GetComponent<BattleController>().isAlive)
        {
            yield break;
        }
        CurrentTarget.GetComponent<BattleController>().Damaged((int)(_atk * 1.5));
    }
}
