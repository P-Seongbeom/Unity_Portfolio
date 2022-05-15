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
            PlayerHpBarList.Add(hpbar);
        }

        for(int i = 0; i < BattleManager.Instance.Phases.Count; ++i)
        {
            for (int j = 0; j < BattleManager.Instance.Phases[i].transform.childCount; ++j)
            {
                GameObject hpbar = Instantiate(_enemyHpBar, BattleManager.Instance.Phases[i].transform.GetChild(j).position, Quaternion.identity, transform);
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

        for (int i = 0; i < BattleManager.Instance.InBattlePlayerPets.Count; ++i)
        {
            PlayerHpBarList[i].transform.position
            = MainCam.WorldToScreenPoint(BattleManager.Instance.InBattlePlayerPets[i].transform.position + new Vector3(0, 4f, 1f));
        }

        if(BattleManager.Instance.inPhase)
        {
            for(int i = 0; i < EnemyHpBarList.Count; ++i)
            {
                if(BattleManager.Instance.InStageEnemy[i].activeSelf)
                {
                    EnemyHpBarList[i].SetActive(true);

                    EnemyHpBarList[i].transform.position
                    = MainCam.WorldToScreenPoint(BattleManager.Instance.InStageEnemy[i].transform.position + new Vector3(0, 4f, 1f));
                }
                else
                {
                    EnemyHpBarList[i].SetActive(false);
                }
            }
        }
    }
}
