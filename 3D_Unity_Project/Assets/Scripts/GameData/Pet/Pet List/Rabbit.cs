using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : PlayerPetBattleController
{
    public GameObject Carrot;

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
            UsingSkill = false;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 rangePos = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
            SkillRange.transform.position = rangePos;
        }

        if (Input.GetMouseButtonUp(0) && BattleUI.Instance.ClickDown)
        {
            UsingSkill = false;

            SkillMotion = true;

            Time.timeScale = 1f;

            BattleUI.Instance.CostUpdate(_skillCost, _skillCooltime);

            SkillRange.SetActive(false);

            StopCoroutine(ChaseTarget());

            StartCoroutine(Feed());
        }

        IEnumerator Feed()
        {
            Controller.SkillMotion();

            for (int i = 0; i < BattleManager.Instance.InBattlePlayerPets.Count; ++i)
            {
                if(BattleManager.Instance.InBattlePlayerPets[i].GetComponent<BattleController>().isAlive)
                {
                    GameObject carrot = Instantiate(Carrot, BulletPos.position, BulletPos.rotation);

                    carrot.GetComponent<Bullet>().Target = BattleManager.Instance.InBattlePlayerPets[i];
                }
            }

            yield return new WaitForSeconds(1f);

            for (int i = 0; i < BattleManager.Instance.InBattlePlayerPets.Count; ++i)
            {
                if(BattleManager.Instance.InBattlePlayerPets[i].GetComponent<BattleController>().isAlive)
                {
                    if (Vector3.Distance(transform.position, BattleManager.Instance.InBattlePlayerPets[i].transform.position) < _skillRange)
                    {
                        BattleManager.Instance.InBattlePlayerPets[i].GetComponent<BattleController>()._hp += _atk;
                    }
                }
            }
        }
    }
}
