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

        foreach (GameObject pet in GameManager.Instance.HavePets)
        {
            Spawn(pet);
        }
    }
    void Start()
    {
        if (DataManager.Instance.QuestLogNumber[0] == 0)
        {
            Communicate(TutorialObject);
        }

        QuestManager.Instance.CheckQuest();
    }

    public void Spawn(GameObject prefab)
    {
        if(prefab.GetComponent<PetController>() == null)
        {
            Debug.LogError("Prefab doesn't have 'PlayerPetController' component.");
        }
        else
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.position = new Vector3(-2, 2, -2);
            PetController controller = obj.GetComponent<PetController>();
            InputHandle.AddController(controller);
            SpawnedPets.Add(controller.gameObject);
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
            Debug.Log(QuestManager.Instance.CheckQuest(id));
            return;
        }

        TalkEffect.SetMessage(talkData);

        isTalk = true;

        ++talkIndex;
    }

    public void EnterStageSelect()
    {
        SceneManager.LoadScene(StageSelectScene);
    }

    public void OnClickQuestButton()
    {
        string title = 
            QuestManager.Instance.QuestData[QuestManager.Instance.QuestDataIndex].QuestName;

        string content =
            QuestManager.Instance.QuestData[QuestManager.Instance.QuestDataIndex].QuestDescription;

        string reward = 
            QuestManager.Instance.QuestData[QuestManager.Instance.QuestDataIndex].RewardGold.ToString();

        FarmUIPopup.Instance.OpenPopup(
            title+"\n", content, $"{reward}°ñµå",
            () => { FarmUIPopup.Instance.ClosePopup(); });
    }
}


