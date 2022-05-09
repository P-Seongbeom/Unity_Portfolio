using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Serialization<T>
{
    public Serialization(List<T> _target) => target = _target;
    public List<T> target;
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public TextAsset StageDatabase;
    public TextAsset PlayerPetDatabase;

    //public StageInfo StageData;
    public List<StageData> AllStageList;
    public List<StageData> OpenStageList;

    public List<PlayerPetData> AllPlayerPet;
    public List<PlayerPetData> HavePlayerPet;

    //public List<Stage> StageData;

    string _stageFilePath;
    string _playerPetFilePath;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        //전체 스테이지 리스트 불러오기
        string[] line = StageDatabase.text.Substring(0, StageDatabase.text.Length - 1).Split('\n');

        for(int i = 0; i < line.Length; ++i)
        {
            string[] row = line[i].Split('\t');

            AllStageList.Add(new StageData(row[0], int.Parse(row[1]), int.Parse(row[2]), bool.Parse(row[3]), bool.Parse(row[4])));
        }

        //전체 펫 리스트 불러오기
        line = PlayerPetDatabase.text.Substring(0, PlayerPetDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; ++i)
        {
            string[] row = line[i].Split('\t');

            AllPlayerPet.Add(new PlayerPetData(row[0], int.Parse(row[1]), bool.Parse(row[2]), row[3], 
                                                int.Parse(row[4]), int.Parse(row[5]), int.Parse(row[6]), 
                                                int.Parse(row[7]), float.Parse(row[8]), float.Parse(row[9])));
        }

        _stageFilePath = Application.persistentDataPath + "/OpenStageList.txt";
        _playerPetFilePath = Application.persistentDataPath + "/HavePlayerPet.txt";

        LoadStageData();
        LoadPlayerPetData();

        print(_playerPetFilePath);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OpenStage(0);
            GetPetCard(0);
            SaveStageData();
            SavePlayerPetData();
        }
        else if(Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetStageData();
            ResetPlayerPetData();
        }
    }

    public void SaveStageData()
    {
        string jdata = JsonUtility.ToJson(new Serialization<StageData>(OpenStageList));

        File.WriteAllText(_stageFilePath, jdata);
        print("스테이지 저장!");
    }
    
    public void LoadStageData()
    {
        if(false == File.Exists(_stageFilePath))
        {
            ResetStageData();
            return;
        }

        string jdata = File.ReadAllText(_stageFilePath);

        OpenStageList = JsonUtility.FromJson<Serialization<StageData>>(jdata).target;
    }

    public void ResetStageData()
    {
        for(int i = 0; i < AllStageList.Count; ++i)
        {
            StageData stage = AllStageList[i];
            stage.OpenStage = false;
        }

        OpenStageList.Clear();

        SaveStageData();

        LoadStageData();

        print("스테이지 리셋!");
    }

    public void OpenStage(int stageNum)
    {
        AllStageList[stageNum].OpenStage = true;
        OpenStageList.Add(AllStageList[stageNum]);
        print("스테이지 열음!");
    }

    public void SavePlayerPetData()
    {
        string jdata = JsonUtility.ToJson(new Serialization<PlayerPetData>(HavePlayerPet));

        File.WriteAllText(_playerPetFilePath, jdata);
        print("펫 저장!");
    }

    public void LoadPlayerPetData()
    {
        if (false == File.Exists(_playerPetFilePath))
        {
            ResetPlayerPetData();
            return;
        }

        string jdata = File.ReadAllText(_playerPetFilePath);

        HavePlayerPet = JsonUtility.FromJson<Serialization<PlayerPetData>>(jdata).target;
    }

    public void ResetPlayerPetData()
    {
        for (int i = 0; i < AllPlayerPet.Count; ++i)
        {
            PlayerPetData pet = AllPlayerPet[i];
            pet.IsGetted = false;
        }

        HavePlayerPet.Clear();

        SavePlayerPetData();

        LoadPlayerPetData();

        print("펫 리셋!");
    }

    public void GetPetCard(int petNum)
    {
        AllPlayerPet[petNum].IsGetted = true;
        HavePlayerPet.Add(AllPlayerPet[petNum]);
        print("펫 얻음!");
    }
}
