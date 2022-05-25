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

    public override void UseSkill()
    {
        if (false == UsingSkill)
        {
            return;
        }

        if (BattleUI.Instance.ClickDown)
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
        if (Input.GetMouseButtonUp(0) && BattleUI.Instance.ClickDown)
        {
            Controller.Agent.velocity = Vector3.zero;

            transform.forward = (hit.point - transform.position).normalized;

            transform.forward = (new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position).normalized;
            desti.transform.position = transform.position + transform.forward * 20f;

            UsingSkill = false;

            SkillMotion = true;

            Time.timeScale = 1f;

            BattleUI.Instance.CostUpdate(_skillCost, _skillCooltime);

            SkillRange.SetActive(false);

            StopCoroutine(ChaseTarget());

            StartCoroutine(Rush());
        }
    }

    IEnumerator Rush()
    {
        Controller.Agent.isStopped = true;
        Controller.Agent.stoppingDistance = 0.5f;
        Controller.Agent.SetDestination(transform.position + transform.forward * 15f);

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
        if(false == SkillMotion)
        {
            return;
        }

        if(other.tag == "Enemy")
        {
            other.GetComponent<BattleController>().Damaged((int)(_atk * 1.5f));
        }
    }
}
