using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyPetDatabase : Database
{
    public List<EnemyPetData> EnemyPet = new List<EnemyPetData>();

    public override void InitData()
    {
        textData = Resources.Load<TextAsset>("Database/EnemyPet_Database");

        string[] line = textData.text.Substring(0, textData.text.Length - 1).Split('\n');

        for (int i = 0; i < line.Length; ++i)
        {
            string[] row = line[i].Split('\t');

            EnemyPet.Add(new EnemyPetData(row[0], int.Parse(row[1]), int.Parse(row[2]),
                                                int.Parse(row[3]), int.Parse(row[4]), float.Parse(row[5]),
                                                float.Parse(row[6]), float.Parse(row[7])));
        }

        path = Application.persistentDataPath + "/EnemyPet.txt";

        Load();
    }

    public override void Load()
    {
        if (false == File.Exists(path))
        {
            Save();

            Load();
            return;
        }

        string jdata = File.ReadAllText(path);

        EnemyPet = JsonUtility.FromJson<Serialization<EnemyPetData>>(jdata).target;
    }

    public override void DataUpdate()
    {
        saveData = JsonUtility.ToJson(new Serialization<EnemyPetData>(EnemyPet));
    }
}
