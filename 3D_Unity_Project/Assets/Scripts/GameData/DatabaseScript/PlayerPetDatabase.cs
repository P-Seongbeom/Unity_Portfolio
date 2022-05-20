using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerPetDatabase : Database
{
    public List<PlayerPetData> AllPlayerPet = new List<PlayerPetData>();
    public List<PlayerPetData> HavePlayerPet = new List<PlayerPetData>();

    public override void InitData()
    {
        textData = Resources.Load<TextAsset>("Database/PlayerPet_Database");

        string[] line = textData.text.Substring(0, textData.text.Length - 1).Split('\n');

        for (int i = 0; i < line.Length; ++i)
        {
            string[] row = line[i].Split('\t');

            AllPlayerPet.Add(new PlayerPetData(row[0], int.Parse(row[1]), bool.Parse(row[2]), row[3],
                                                int.Parse(row[4]), int.Parse(row[5]), int.Parse(row[6]),
                                                int.Parse(row[7]), float.Parse(row[8]), float.Parse(row[9]),
                                                float.Parse(row[10]), int.Parse(row[11])));
        }

        path = Application.persistentDataPath + "/HavePlayerPet.txt";

        Load();
    }

    public override void Load()
    {
        if (false == File.Exists(path))
        {
            for (int i = 0; i < AllPlayerPet.Count; ++i)
            {
                PlayerPetData pet = AllPlayerPet[i];
                pet.IsGetted = false;
            }
            HavePlayerPet.Clear();

            Save();

            Load();

            return;
        }

        string jdata = File.ReadAllText(path);

        HavePlayerPet = JsonUtility.FromJson<Serialization<PlayerPetData>>(jdata).target;
    }

    public override void DataUpdate()
    {
        saveData = JsonUtility.ToJson(new Serialization<PlayerPetData>(HavePlayerPet));
    }

    public void GetPetCard(int petNum)
    {
        for (int i = 0; i < HavePlayerPet.Count; ++i)
        {
            if (AllPlayerPet[petNum].PetNumber == HavePlayerPet[i].PetNumber)
            {
                return;
            }
        }

        AllPlayerPet[petNum].IsGetted = true;
        HavePlayerPet.Add(AllPlayerPet[petNum]);
        HavePlayerPet.Sort(delegate (PlayerPetData a, PlayerPetData b) { return a.PetNumber.CompareTo(b.PetNumber); });

        Save();
        Load();

        GameManager.Instance.HavingPetUpdate();

        if (FarmManager.Instance != null)
        {
            FarmManager.Instance.RealTimePetUpdate();
        }

    }

}
