using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : PlayerPetBattleController
{
    Ray ray;
    RaycastHit hit;

    public GameObject desti;
    public GameObject agentdes;

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

    //public override void UseSkill()
    //{
    //    if (false == UsingSkill))
    //    {
    //        return;
    //    }
    //    if (Input.GetMouseButton(0))
    //    {
    //        Vector3 rangePos = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
    //        SkillRange.transform.position = rangePos;
    //        SkillRange.SetActive(true);
    //    }
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        UsingSkill = true;
    //
    //        SkillMotion = true;

    //        SkillRange.SetActive(false);

    //        Time.timeScale = 1f;

    //        BattleUI.Instance.CostUpdate(_skillCost, _skillCooltime);

    //        StartCoroutine(Bite());
    //    }
    //}

    public override void UseSkill()
    {
        if (false == UsingSkill)
        {
            return;
        }

        if (BattleUI.Instance.CkickDown)
        {
            SkillRange.SetActive(true);
        }
        else
        {
            SkillRange.SetActive(false);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 rangePos = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
            SkillRange.transform.position = rangePos;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "Ground")
                {
                    SkillRange.transform.forward = (hit.point - rangePos).normalized;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Controller.Agent.velocity = Vector3.zero;

            transform.forward = (hit.point - transform.position).normalized;

            transform.forward = (new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position).normalized;
            desti.transform.position = transform.position + transform.forward * 20f;

            UsingSkill = false;

            SkillMotion = true;

            Time.timeScale = 1f;

            BattleUI.Instance.CostUpdate(_skillCost, _skillCooltime);
            StopCoroutine(ChaseTarget());
            StartCoroutine(Rush());
        }
    }

    //IEnumerator Bite()
    //{
    //    Controller.SkillMotion();

    //    yield return new WaitForSeconds(1f);

    //    if(false == CurrentTarget.GetComponent<BattleController>().isAlive)
    //    {
    //        yield break;
    //    }
    //    CurrentTarget.GetComponent<BattleController>().Damaged((int)(_atk * 1.5));
    //}

    IEnumerator Rush()
    {
        Controller.Agent.isStopped = true;
        Controller.Agent.stoppingDistance = 0.5f;
        Controller.Agent.SetDestination(transform.position + transform.forward * 10f);

        Controller.SkillMotion();

        yield return new WaitForSeconds(0.5f);

        Controller.Agent.isStopped = false;
        Controller.Agent.speed = 15f;

        yield return new WaitForSeconds(0.5f);

        Controller.Agent.stoppingDistance = _attackRange;
        Controller.Agent.speed = 9f;

        StartCoroutine(ChaseTarget());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<BattleController>()._hp -= (int)(_atk * 1.5f);
        }
    }
}
