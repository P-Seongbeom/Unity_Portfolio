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
            DataManager.Instance.RenewQuestLog(QuestId, QuestActionIndex);
        }

        if(QuestActionIndex == QuestList[QuestId].NpcId.Length)
        {
            QuestClear();
            DataManager.Instance.RenewQuestLog(QuestId, QuestActionIndex);
        }

        return QuestList[QuestId].QuestName;
    }

    public string CheckQuest()
    {
        return QuestList[QuestId].QuestName;
    }

    //퀘스트 클리어
    public void QuestClear()
    {
        //보상
        GetQuestReward();

        //다음 퀘스트
        NextQuest();

    }

    void NextQuest()
    {
        QuestId += 10;
        QuestActionIndex = 0;
    }

    public void GetQuestReward()
    {
        DataManager.Instance.GetGold(QuestList[QuestId].RewardGold);

        if(QuestList[QuestId].RewardCard.Length > 0)
        {
            for(int i = 0; i < QuestList[QuestId].RewardCard.Length; ++i)
            {
                if(DataManager.Instance.AllPlayerPet[i].PetName == QuestList[QuestId].RewardCard[i])
                {
                    DataManager.Instance.GetPetCard(DataManager.Instance.AllPlayerPet[i].PetNumber);
                }
            }
        }

        //보상 알림 메세지

    }

    //임시
    void ControlObject()
    {
        switch(QuestId)
        {
            case 10:
                if(QuestActionIndex == 2)
                {
                    
                }
                break;
            case 20:

                break;
        }
    }
    
    
}
