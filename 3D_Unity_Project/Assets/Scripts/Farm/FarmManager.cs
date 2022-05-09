using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FarmManager : MonoBehaviour
{
    public static FarmManager Instance;
    public InputManager InputHandle;

    public List<GameObject> PetPrefabs;
    public List<GameObject> SpawnedPets;

    public GameObject TalkBox;
    public Text TalkText;
    public Image PortraitImage;
    public GameObject ScanObject;
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

        foreach (GameObject pet in PetPrefabs)
        {
            if (pet == null)
            {
                PetPrefabs.Remove(pet);
            }
        }

        foreach (GameObject pet in PetPrefabs)
        {
            
            Spawn(pet);
        }
    }
    void Start()
    {
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

    public void EnterStageSelect()
    {
        SceneManager.LoadScene(StageSelectScene);
    }

    public void Communicate(GameObject scanObject)
    {
        ScanObject = scanObject;
        
        TalkData data = ScanObject.GetComponent<NPCData>()._dialogueData;

        PortraitImage.sprite = ScanObject.GetComponent<NPCData>().Portrait;

        Talk(data.TalkId);

        TalkBox.SetActive(isTalk);
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

        TalkText.text = talkData;

        isTalk = true;

        ++talkIndex;
    }
}


