using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public QuestData[] QuestData;

    public int QuestId;
    public int QuestActionIndex;

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

    void GenerateData(int questId, int index)
    {
        //QuestList.Add(10, new QuestData("마을 사람들과 대화하기", 
        //                                new int[] { 100, 200 }));
        //QuestList.Add(20, new QuestData("돼지와 대화하기",
        //                                new int[] { 100, 200 }));

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

    void NextQuest()
    {
        QuestId += 10;
        QuestActionIndex = 0;
    }
}
