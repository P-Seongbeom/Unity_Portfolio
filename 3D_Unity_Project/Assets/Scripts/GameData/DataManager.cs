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
    public TextAsset QuestLogDatabase;
    public TextAsset PlayerDatabase;

    //public StageInfo StageData;
    public List<StageData> AllStageList;
    public List<StageData> OpenStageList;

    public List<PlayerPetData> AllPlayerPet;
    public List<PlayerPetData> HavePlayerPet;

    public List<int> QuestLogNumber;
    public PlayerData PlayerData;

    //public List<Stage> StageData;

    string _stageFilePath;
    string _playerPetFilePath;
    string _questLogFilePath;
    string _playerFilePath;

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

        //전체 스테이지 리스트 불러오기
        string[] line = StageDatabase.text.Substring(0, StageDatabase.text.Length - 1).Split('\n');

        for (int i = 0; i < line.Length; ++i)
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
                                                int.Parse(row[7]), float.Parse(row[8]), float.Parse(row[9]),
                                                int.Parse(row[10])));
        }

        //퀘스트 진행상황 불러오기
        line = QuestLogDatabase.text.Substring(0, QuestLogDatabase.text.Length - 1).Split('\t');
        for (int i = 0; i < line.Length; ++i)
        {
            QuestLogNumber.Add(int.Parse(line[i]));
        }

        //플레이어 데이터 불러오기
        line = PlayerDatabase.text.Substring(0, PlayerDatabase.text.Length - 1).Split('\t');

        PlayerData = new PlayerData(line[0], int.Parse(line[1]), int.Parse(line[2]));
        
    }

    void Start()
    {
        _stageFilePath = Application.persistentDataPath + "/OpenStageList.txt";
        _playerPetFilePath = Application.persistentDataPath + "/HavePlayerPet.txt";
        _questLogFilePath = Application.persistentDataPath + "/QuestNumber.txt";
        _playerFilePath = Application.persistentDataPath + "/Player.txt";

        LoadStageData();
        LoadPlayerPetData();
        LoadQuestLog();
        LoadPlayerData();

        print(_playerPetFilePath);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OpenStage(0);
            GetPetCard(0);
            RenewQuestLog(100, 100);
            RenewPlayer("릴파넴", 700, 700);
            SaveStageData();
            SavePlayerPetData();
            SaveQuestLog();
        }
        else if(Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetStageData();
            ResetPlayerPetData();
            ResetQuestLog();
        }
        else if(Input.GetKeyDown(KeyCode.Insert))
        {
            OpenStage(1);
        }
    }
    #region StageData
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
    #endregion

    #region PlayerPetData
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
    #endregion

    #region QuestLog
    public void SaveQuestLog()
    {
        string jdata = JsonUtility.ToJson(new Serialization<int>(QuestLogNumber));

        File.WriteAllText(_questLogFilePath, jdata);
        print("퀘스트 저장!");
    }

    public void LoadQuestLog()
    {
        if (false == File.Exists(_questLogFilePath))
        {
            ResetQuestLog();
            return;
        }

        string jdata = File.ReadAllText(_questLogFilePath);

        QuestLogNumber = JsonUtility.FromJson<Serialization<int>>(jdata).target;
    }

    public void ResetQuestLog()
    {
        RenewQuestLog(0, 0);

        SaveQuestLog();

        LoadQuestLog();

        print("퀘스트 리셋!");
    }

    public void RenewQuestLog(int questId, int actionIndex)
    {
        QuestLogNumber[0] = questId;
        QuestLogNumber[1] = actionIndex;
    }
    #endregion

    #region PlayerData
    public void SavePlayerData()
    {
        string jdata = JsonUtility.ToJson(PlayerData);

        File.WriteAllText(_playerFilePath, jdata);
        print("플레이어 저장!");
    }

    public void LoadPlayerData()
    {
        if (false == File.Exists(_playerFilePath))
        {
            ResetPlayerData();
            return;
        }

        string jdata = File.ReadAllText(_playerFilePath);

        PlayerData = JsonUtility.FromJson<PlayerData>(jdata);
    }

    public void ResetPlayerData()
    {
        string[] line = PlayerDatabase.text.Substring(0, PlayerDatabase.text.Length - 1).Split('\t');

        RenewPlayer(line[0], int.Parse(line[1]), int.Parse(line[2]));

        SavePlayerData();

        LoadPlayerData();

        print("플레이어 리셋!");
    }

    public void RenewPlayer(string name, int num, int gold)
    {
        PlayerData.PlayerName = name;
        PlayerData.PlayerNum = num;
        PlayerData.Gold = gold;
    }

    #endregion
}
