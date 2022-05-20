using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestLogData : Database
{
    public List<int> QuestLogNumber = new List<int>();

    public override void InitData()
    {
        textData = Resources.Load<TextAsset>("Database/Quest_Database");

        string[] line = textData.text.Substring(0, textData.text.Length - 1).Split('\t');
        for (int i = 0; i < line.Length; ++i)
        {
            QuestLogNumber.Add(int.Parse(line[i]));
        }

        path = Application.persistentDataPath + "/QuestNumber.txt";

        Load();
    }

    public override void Load()
    {
        if (false == File.Exists(path))
        {
            RenewQuestLog(0, 0);
            return;
        }

        string jdata = File.ReadAllText(path);

        QuestLogNumber = JsonUtility.FromJson<Serialization<int>>(jdata).target;
    }

    public override void DataUpdate()
    {
        saveData = JsonUtility.ToJson(new Serialization<int>(QuestLogNumber));
    }

    public void RenewQuestLog(int questId, int actionIndex)
    {
        QuestLogNumber[0] = questId;
        QuestLogNumber[1] = actionIndex;

        Save();
        Load();

        QuestManager.Instance.QuestNumberUpdate();
    }
}
