using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public static BattleUI Instance;

    public List<Button> PetSkills;

    public Slider CostBar;
    [SerializeField]
    private Text _currentCostText;

    public List<GameObject> PlayerHpBarList;
    public List<GameObject> EnemyHpBarList;
    [SerializeField]
    private GameObject _playerHpBar;
    [SerializeField]
    private GameObject _enemyHpBar;

    public Camera MainCam;

    public GameObject StageOverUI;
    [SerializeField]
    private Text ClearText;
    [SerializeField]
    private Text FailText;
    [SerializeField]
    private GameObject GoFarmButton;

    private int _selectedIndex;

    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        MainCam = Camera.main;

        for (int i = 0; i < PetSkills.Count; ++i)
        {
            PetSkills[i].image.sprite = BattleManager.Instance.InBattlePlayerPets[i].GetComponent<PetInfo>().CardPortrait;
            PetSkills[i].GetComponentInChildren<Text>().text = BattleManager.Instance.InBattlePlayerPets[i].GetComponent<PetInfo>().SkillCost.ToString();
        }

        for(int i = 0; i < BattleManager.Instance.InBattlePlayerPets.Count; ++i)
        {
            GameObject hpbar = Instantiate(_playerHpBar, BattleManager.Instance.InBattlePlayerPets[i].transform.position, Quaternion.identity, transform);
            hpbar.GetComponent<Slider>().maxValue = BattleManager.Instance.InBattlePlayerPets[i].GetComponent<BattleController>()._hp;
            PlayerHpBarList.Add(hpbar);
        }

        for(int i = 0; i < BattleManager.Instance.Phases.Count; ++i)
        {
            for (int j = 0; j < BattleManager.Instance.Phases[i].transform.childCount; ++j)
            {
                GameObject hpbar = Instantiate(_enemyHpBar, BattleManager.Instance.Phases[i].transform.GetChild(j).position, Quaternion.identity, transform);
                hpbar.GetComponent<Slider>().maxValue = BattleManager.Instance.Phases[i].transform.GetChild(j).GetComponent<BattleController>()._hp;
                EnemyHpBarList.Add(hpbar);
            }
        }

        for(int i = 0; i < EnemyHpBarList.Count; ++i)
        {
            EnemyHpBarList[i].SetActive(false);
        }

    }

    void Update()
    {
        _currentCostText.text = ((int)BattleManager.Instance.OverallCost).ToString();
        CostBar.value = BattleManager.Instance.OverallCost;
        RenderPlayerHp();
        RenderEnemyHp();
        CheckCost();
    }

    void RenderPlayerHp()
    {

        for (int i = 0; i < BattleManager.Instance.InBattlePlayerPets.Count; ++i)
        {

            if (BattleManager.Instance.InBattlePlayerPets[i].activeSelf)
            {
                PlayerHpBarList[i].transform.position
                = MainCam.WorldToScreenPoint(BattleManager.Instance.InBattlePlayerPets[i].transform.position + new Vector3(0, 4f, 1f));

                if(BattleManager.Instance.InBattlePlayerPets[i].GetComponent<BattleController>()._hp > 0)
                {
                    PlayerHpBarList[i].GetComponent<Slider>().value = BattleManager.Instance.InBattlePlayerPets[i].GetComponent<BattleController>()._hp;
                }
                else
                {
                    PlayerHpBarList[i].transform.Find("Fill Area").gameObject.SetActive(false);
                }
            }
            else
            {
                PlayerHpBarList[i].SetActive(false);
            }
        }
    }

    void RenderEnemyHp()
    {
        if (BattleManager.Instance.inPhase)
        {
            for (int i = 0; i < EnemyHpBarList.Count; ++i)
            {
                if (BattleManager.Instance.InStageEnemy[i].activeSelf)
                {
                    EnemyHpBarList[i].SetActive(true);

                    EnemyHpBarList[i].transform.position
                    = MainCam.WorldToScreenPoint(BattleManager.Instance.InStageEnemy[i].transform.position + new Vector3(0, 4f, 1f));

                    if (BattleManager.Instance.InStageEnemy[i].GetComponent<BattleController>()._hp > 0)
                    {
                        EnemyHpBarList[i].GetComponent<Slider>().value = BattleManager.Instance.InStageEnemy[i].GetComponent<BattleController>()._hp;
                    }
                    else
                    {
                        EnemyHpBarList[i].transform.Find("Fill Area").gameObject.SetActive(false);
                    }
                }
                else
                {
                    EnemyHpBarList[i].SetActive(false);
                }
            }
        }
    }

    public void ClickSkillButton(int uiNum)
    {
        Time.timeScale = 0.5f;
        _selectedIndex = uiNum;
        BattleManager.Instance.InBattlePlayerPets[uiNum].GetComponent<BattleController>().UsingSkill = true;
    }


    public void CostUpdate(int cost, float cooltime)
    {
        BattleManager.Instance.OverallCost -= cost;
        print(cooltime);
        StartCoroutine(BlockButton(_selectedIndex, cooltime));
    }

    public void CheckCost()
    {
        for(int i = 0; i < BattleManager.Instance.InBattlePlayerPets.Count; ++i)
        {
            if(BattleManager.Instance.InBattlePlayerPets[i].GetComponent<BattleController>().SkillReady)
            {
                if(BattleManager.Instance.OverallCost < BattleManager.Instance.InBattlePlayerPets[i].GetComponent<BattleController>()._skillCost
                    || false == BattleManager.Instance.InBattlePlayerPets[i].GetComponent<BattleController>().isAlive)
                {
                    PetSkills[i].interactable = false;
                }
                else if(BattleManager.Instance.OverallCost >= BattleManager.Instance.InBattlePlayerPets[i].GetComponent<BattleController>()._skillCost
                    && BattleManager.Instance.InBattlePlayerPets[i].GetComponent<BattleController>().isAlive)
                {
                    PetSkills[i].interactable = true;
                }
            }
        }
    }

    public IEnumerator BlockButton(int uiNum,float time)
    {
        PetSkills[uiNum].interactable = false;
        BattleManager.Instance.InBattlePlayerPets[uiNum].GetComponent<BattleController>().SkillReady = false;

        yield return new WaitForSeconds(time);

        PetSkills[uiNum].interactable = true;
        BattleManager.Instance.InBattlePlayerPets[uiNum].GetComponent<BattleController>().SkillReady = true;
    }

    public void ClearPhrase()
    {
        GoFarmButton.SetActive(true);
        ClearText.gameObject.SetActive(true);
    }

    public void FailPhrase()
    {
        GoFarmButton.SetActive(true);
        FailText.gameObject.SetActive(true);
    }

}
