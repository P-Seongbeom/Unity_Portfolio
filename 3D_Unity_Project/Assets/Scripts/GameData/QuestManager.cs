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
        QuestNumberUpdate();
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
            DataManager.Instance.QuestLog.RenewQuestLog(QuestId, QuestActionIndex);
        }

        if(QuestActionIndex == QuestList[QuestId].NpcId.Length)
        {
            QuestClear();
            DataManager.Instance.QuestLog.RenewQuestLog(QuestId, QuestActionIndex);
        }

        return QuestList[QuestId].QuestName;
    }

    public string CheckQuest()
    {
        return QuestList[QuestId].QuestName;
    }

    public void QuestClear()
    {
        GetQuestReward();

        NextQuest();
    }

    void NextQuest()
    {
        QuestId += 10;
        QuestActionIndex = 0;
    }

    public void GetQuestReward()
    {
        DataManager.Instance.PlayerData.GetGold(QuestList[QuestId].RewardGold);

        if (QuestList[QuestId].RewardCard.Length > 0)
        {
            for(int i = 0; i < QuestList[QuestId].RewardCard.Length; ++i)
            {
                foreach(PlayerPetData data in DataManager.Instance.MyPetData.AllPlayerPet)
                {
                    if(data.PetName == QuestList[QuestId].RewardCard[i])
                    {
                        DataManager.Instance.MyPetData.GetPetCard(data.PetNumber);
                    }
                }
            }
        }

        if(QuestList[QuestId].OpenStageNum != "-")
        {
            DataManager.Instance.StageData.OpenStage(int.Parse(QuestList[QuestId].OpenStageNum));
        }
    }
    
    public void QuestNumberUpdate()
    {
        if (DataManager.Instance.QuestLog.QuestLogNumber[0] == 0)
        {
            QuestId = 10;
            QuestActionIndex = 0;
        }
        else
        {
            QuestId = DataManager.Instance.QuestLog.QuestLogNumber[0];
            QuestActionIndex = DataManager.Instance.QuestLog.QuestLogNumber[1];
        }
    }
}
