using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StageDatabase : Database
{
    public List<StageData> AllStageList = new List<StageData>();
    public List<StageData> OpenStageList = new List<StageData>();

    public override void InitData()
    {
        textData = Resources.Load<TextAsset>("Database/Stage_Database");

        string[] line = textData.text.Substring(0, textData.text.Length - 1).Split('\n');

        for (int i = 0; i < line.Length; ++i)
        {
            string[] row = line[i].Split('\t');

            AllStageList.Add(new StageData(row[0], int.Parse(row[1]), int.Parse(row[2]), bool.Parse(row[3]), bool.Parse(row[4]), int.Parse(row[5])));
        }

        path = Application.persistentDataPath + "/OpenStageList.txt";

        Load();
    }

    public override void Load()
    {
        if (false == File.Exists(path))
        {
            for (int i = 0; i < AllStageList.Count; ++i)
            {
                StageData stage = AllStageList[i];
                stage.OpenStage = false;
            }

            OpenStageList.Clear();

            Save();
            Load();

            return;
        }

        string jdata = File.ReadAllText(path);

        OpenStageList = JsonUtility.FromJson<Serialization<StageData>>(jdata).target;
    }

    public override void DataUpdate()
    {
        saveData = JsonUtility.ToJson(new Serialization<StageData>(OpenStageList));
    }

    public void OpenStage(int stageNum)
    {
        for(int i = 0; i < OpenStageList.Count; ++i)
        {
            if(OpenStageList[i].StageNumber == AllStageList[stageNum].StageNumber)
            {
                return;
            }
        }

        AllStageList[stageNum].OpenStage = true;

        OpenStageList.Add(AllStageList[stageNum]);

        Save();
        Load();
    }
}
