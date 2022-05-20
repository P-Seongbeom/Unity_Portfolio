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

    public List<GameObject> temp;

    public BGMPlayer BgmPlayer;

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
        temp = SpawnedPets;

        foreach (GameObject pet in GameManager.Instance.HavePets)
        {
            Spawn(pet);
        }

        if (DataManager.Instance.QuestLog.QuestLogNumber[0] == 0)
        {
            Communicate(TutorialObject);
        }

        QuestManager.Instance.CheckQuest();
    }

    public void RealTimePetUpdate()
    {
        foreach (GameObject pet in GameManager.Instance.HavePets)
        {
            Spawn(pet);
        }
    }

    public void Spawn(GameObject prefab)
    {
        if(prefab.GetComponent<PetController>() == null)
        {
            Debug.LogError("Prefab doesn't have 'PlayerPetController' component.");
        }

        if (SpawnedPets.Count == 0)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.position = new Vector3(-2, 2, -2);
            PetController controller = obj.GetComponent<PetController>();
            InputHandle.AddController(controller);
            SpawnedPets.Add(controller.gameObject);
        }
        else
        {
            bool ishave = false;
            for (int i = 0; i < SpawnedPets.Count; ++i)
            {
                if (SpawnedPets[i].GetComponent<PetInfo>().PetName == prefab.GetComponent<PetInfo>().PetName)
                {
                    ishave = true;
                }
            }
            if(false == ishave)
            {
                GameObject spawnpet = Instantiate(prefab, SpawnedPets[0].transform.position + SpawnedPets[0].transform.forward * 5f, Quaternion.identity);
                PetController controller = spawnpet.GetComponent<PetController>();
                InputHandle.AddController(controller);

                SpawnedPets.Add(controller.gameObject);
            }
        }
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
        int questTalkIndex = QuestManager.Instance.GetQuestTalkIndex(id);

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
}


