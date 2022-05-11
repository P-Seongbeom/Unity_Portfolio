using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public QuestData[] QuestData;

    public int QuestId;
    public int QuestActionIndex;
    public int QuestDataIndex;

    Dictionary<int, QuestData> QuestList;

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
        DontDestroyOnLoad(this.gameObject);

        QuestList = new Dictionary<int, QuestData>();

        for(int i = 0; i < QuestData.Length; ++i)
        {
            GenerateData(10 + (i * 10), i);
        }
    }

    private void Start()
    {
        print(DataManager.Instance.QuestLogNumber[0]);
        if (DataManager.Instance.QuestLogNumber[0] == 0)
        {
            QuestId += 10;
        }
        else
        {
            QuestId = DataManager.Instance.QuestLogNumber[0];
            QuestActionIndex = DataManager.Instance.QuestLogNumber[1];
        }
    }
    private void Update()
    {
        QuestDataIndex = (QuestId / 10) - 1;
    }

    void GenerateData(int questId, int index)
    {
        QuestList.Add(questId, QuestData[index]);
    }

    public int GetQuestTalkIndex(int id)
    {
        return QuestId + QuestActionIndex;
    }

    public string CheckQuest(int id)
    {
        if(id == QuestList[QuestId].NpcId[QuestActionIndex])
        {
            ++QuestActionIndex;
        }

        if(QuestActionIndex == QuestList[QuestId].NpcId.Length)
        {
            NextQuest();
        }

        return QuestList[QuestId].QuestName;
    }

    public string CheckQuest()
    {
        return QuestList[QuestId].QuestName;
    }

    void NextQuest()
    {
        QuestId += 10;
        QuestActionIndex = 0;
    }

    //юс╫ц
    void ControlObject()
    {
        switch(QuestId)
        {
            case 10:
                if(QuestActionIndex == 2)
                {
                    //DataManager.Instance.OpenStage(0);
                }
                break;
            case 20:

                break;
        }
    }
}
