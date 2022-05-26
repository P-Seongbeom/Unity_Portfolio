using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FarmManager : MonoBehaviour
{
    public static FarmManager Instance;
    public InputManager InputHandle;

    public List<GameObject> SpawnedPets;
    public GameObject ScanObject;
    public GameObject TutorialObject;

    public Animator TalkBox;

    public TypingEffect TalkEffect;

    public Image PortraitImage;

    public bool isTalk;
    public int talkIndex;

    public string StageSelectScene;

    public Text _goldText;
    public Text _playerName;

    public BGMPlayer BgmPlayer;

    public GameObject ResetButton;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _playerName.text = DataManager.Instance.PlayerData.Player.PlayerName;
        _goldText.text = DataManager.Instance.PlayerData.Player.Gold.ToString();
    }
    void Start()
    {
        foreach (GameObject pet in GameManager.Instance.HavePets)
        {
            Spawn(pet);
        }

        if (DataManager.Instance.QuestLog.QuestLogNumber[0] == 0)
        {
            Communicate(TutorialObject);
        }
    }

    public void PetUpdate()
    {
        foreach (GameObject pet in GameManager.Instance.HavePets)
        {
            Spawn(pet);
        }
        SpawnedPets.Sort(delegate (GameObject a, GameObject b) { return a.GetComponent<PetInfo>().PetNumber.CompareTo(b.GetComponent<PetInfo>().PetNumber); });
    }

    public void Spawn(GameObject prefab)
    {
        if (SpawnedPets.Count == 0)
        {
            GeneratePet(prefab, new Vector3(-2, 2, -2));
        }
        else
        {
            for (int i = 0; i < SpawnedPets.Count; ++i)
            {
                if (SpawnedPets[i].GetComponent<PetInfo>().PetName == prefab.GetComponent<PetInfo>().PetName)
                {
                    return;
                }
            }
            GeneratePet(prefab, SpawnedPets[0].transform.position + SpawnedPets[0].transform.forward * 5f);
        }
    }

    void GeneratePet(GameObject obj, Vector3 pos)
    {
        GameObject pet = Instantiate(obj, pos, Quaternion.identity);
        PetController controller = pet.GetComponent<PetController>();
        InputHandle.AddController(controller);
        SpawnedPets.Add(controller.gameObject);
    }

    public void Communicate(GameObject scanObject)
    {
        ScanObject = scanObject;

        PortraitImage.sprite = ScanObject.GetComponent<NPCData>().Portrait;

        Talk(ScanObject.GetComponent<NPCData>().NpcId);

        TalkBox.SetBool("isShow", isTalk);
    }

    void Talk(int id)
    {
        int questTalkIndex = QuestManager.Instance.GetQuestTalkIndex();

        string talkData = TalkManager.Instance.GetDialogue(id + questTalkIndex, talkIndex);
        if (talkData == null)
        {
            isTalk = false;
            talkIndex = 0;
            QuestManager.Instance.CheckQuest(id);
            return;
        }

        TalkEffect.SetMessage(talkData);

        isTalk = true;

        ++talkIndex;
    }

    public void OnClickQuestButton()
    {
        string title = 
            QuestManager.Instance.QuestData[QuestManager.Instance.QuestDataIndex].QuestName;

        string content =
            QuestManager.Instance.QuestData[QuestManager.Instance.QuestDataIndex].QuestDescription;

        string reward = 
            QuestManager.Instance.QuestData[QuestManager.Instance.QuestDataIndex].RewardGold.ToString();

        QuestPopup.Instance.OpenPopup(title+"\n", content, $"{reward}°ñµå",
            () => { QuestPopup.Instance.ClosePopup(QuestPopup.Instance.Animator[0]); });

    }

    public void GoldTextUpdate()
    {
        _goldText.text = DataManager.Instance.PlayerData.Player.Gold.ToString();
    }

    public void OnClickCardButton()
    {
        CardInvenPopup.Instance.OpenCardInven(SpawnedPets,() => { CardInvenPopup.Instance.ClosePopup(CardInvenPopup.Instance.Animator[0]); });
    }

    public void EnterStageSelect()
    {
        SceneManager.LoadScene(StageSelectScene);
    }

    public void OnClickPlayerName()
    {
        ResetButton.SetActive(true);
    }

    public void ResetAllData()
    {
        DataManager.Instance.ResetAllData();
    }

    public void CancelReset()
    {
        ResetButton.SetActive(false);
    }


}


