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

    private TextAsset StageDatabase;
    private TextAsset PlayerPetDatabase;
    private TextAsset EnemyPetDatabase;
    private TextAsset QuestLogDatabase;
    private TextAsset PlayerDatabase;

    public List<StageData> AllStageList;
    public List<StageData> OpenStageList;
    public List<PlayerPetData> AllPlayerPet;
    public List<PlayerPetData> HavePlayerPet;
    public List<EnemyPetData> EnemyPet;

    public List<int> QuestLogNumber;
    public PlayerData PlayerData;

    string _stageFilePath;
    string _playerPetFilePath;
    string _enemyPetFilePath;
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

        StageDatabase = Resources.Load<TextAsset>("Database/Stage_Database");
        PlayerPetDatabase = Resources.Load<TextAsset>("Database/PlayerPet_Database");
        EnemyPetDatabase = Resources.Load<TextAsset>("Database/EnemyPet_Database");
        QuestLogDatabase = Resources.Load<TextAsset>("Database/Quest_Database");
        PlayerDatabase = Resources.Load<TextAsset>("Database/Player_Database");

        //전체 스테이지 리스트 불러오기
        string[] line = StageDatabase.text.Substring(0, StageDatabase.text.Length - 1).Split('\n');

        for (int i = 0; i < line.Length; ++i)
        {
            string[] row = line[i].Split('\t');

            AllStageList.Add(new StageData(row[0], int.Parse(row[1]), int.Parse(row[2]), bool.Parse(row[3]), bool.Parse(row[4]), int.Parse(row[5])));
        }

        //전체 펫 리스트 불러오기
        line = PlayerPetDatabase.text.Substring(0, PlayerPetDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; ++i)
        {
            string[] row = line[i].Split('\t');

            AllPlayerPet.Add(new PlayerPetData(row[0], int.Parse(row[1]), bool.Parse(row[2]), row[3],
                                                int.Parse(row[4]), int.Parse(row[5]), int.Parse(row[6]),
                                                int.Parse(row[7]), float.Parse(row[8]), float.Parse(row[9]),
                                                float.Parse(row[10]), int.Parse(row[11])));
        }

        //적 펫 리스트 불러오기
        line = EnemyPetDatabase.text.Substring(0, EnemyPetDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; ++i)
        {
            string[] row = line[i].Split('\t');

            EnemyPet.Add(new EnemyPetData(row[0], int.Parse(row[1]), int.Parse(row[2]),
                                                int.Parse(row[3]), int.Parse(row[4]), float.Parse(row[5]),
                                                float.Parse(row[6]), float.Parse(row[7])));
        }

        //퀘스트 진행상황 불러오기
        line = QuestLogDatabase.text.Substring(0, QuestLogDatabase.text.Length - 1).Split('\t');
        for (int i = 0; i < line.Length; ++i)
        {
            QuestLogNumber.Add(int.Parse(line[i]));
        }

        //플레이어 데이터 불러오기
        line = PlayerDatabase.text.Substring(0, PlayerDatabase.text.Length - 1).Split('\t');

        PlayerData = new PlayerData(line[0], int.Parse(line[1]));
        
        _stageFilePath = Application.persistentDataPath + "/OpenStageList.txt";
        _playerPetFilePath = Application.persistentDataPath + "/HavePlayerPet.txt";
        _enemyPetFilePath = Application.persistentDataPath + "/EnemyPet.txt";
        _questLogFilePath = Application.persistentDataPath + "/QuestNumber.txt";
        _playerFilePath = Application.persistentDataPath + "/Player.txt";

    }

    void Start()
    {
        LoadStageData();
        LoadPlayerPetData();
        LoadEnemyPetData();
        LoadQuestLog();
        LoadPlayerData();

        print(_stageFilePath);

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            OpenStage(0);
            GetPetCard(6);
            GetPetCard(2);
            RenewQuestLog(50, 0);
            SetPlayer("릴파넴", 700);
        }
        else if(Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetStageData();
            ResetPlayerPetData();
            ResetQuestLog();
            ResetPlayerData();
        }
        else if(Input.GetKeyDown(KeyCode.Home))
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
        SaveStageData();
        LoadStageData();
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
        for(int i = 0; i < HavePlayerPet.Count; ++i)
        {
            if(AllPlayerPet[petNum].PetNumber == HavePlayerPet[i].PetNumber)
            {
                return;
            }
        }
        
        AllPlayerPet[petNum].IsGetted = true;
        HavePlayerPet.Add(AllPlayerPet[petNum]);
        HavePlayerPet.Sort(delegate (PlayerPetData a, PlayerPetData b) { return a.PetNumber.CompareTo(b.PetNumber); });

        print("펫 얻음!");
        SavePlayerPetData();
        LoadPlayerPetData();

        GameManager.Instance.HavingPetUpdate();

        if(FarmManager.Instance != null)
        {
            FarmManager.Instance.RealTimePetUpdate();
        }

    }
    #endregion

    #region Enemy
    public void SaveEnemyPetData()
    {
        string jdata = JsonUtility.ToJson(new Serialization<EnemyPetData>(EnemyPet));

        File.WriteAllText(_enemyPetFilePath, jdata);
        print("적 펫 저장!");
    }

    public void LoadEnemyPetData()
    {
        if (false == File.Exists(_enemyPetFilePath))
        {
            ResetEnemyPetData();
            return;
        }

        string jdata = File.ReadAllText(_enemyPetFilePath);

        EnemyPet = JsonUtility.FromJson<Serialization<EnemyPetData>>(jdata).target;
    }

    public void ResetEnemyPetData()
    {
        SaveEnemyPetData();

        LoadEnemyPetData();

        print("적 펫 리셋!");
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

        print("퀘스트 리셋!");
    }

    public void RenewQuestLog(int questId, int actionIndex)
    {
        QuestLogNumber[0] = questId;
        QuestLogNumber[1] = actionIndex;

        SaveQuestLog();
        LoadQuestLog();

        QuestManager.Instance.QuestNumberUpdate();
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

        SetPlayer(line[0], int.Parse(line[1]));

        print("플레이어 리셋!");
    }

    public void SetPlayer(string name, int gold)
    {
        PlayerData.PlayerName = name;
        PlayerData.Gold = gold;

        SavePlayerData();
        LoadPlayerData();

        if(null == FarmManager.Instance)
        {
            return;
        }
        FarmManager.Instance._playerName.text = PlayerData.PlayerName;
        FarmManager.Instance._goldText.text = PlayerData.Gold.ToString();
    }

    public void GetGold(int gold)
    {
        PlayerData.Gold += gold;

        SavePlayerData();
        LoadPlayerData();

        if (null == FarmManager.Instance)
        {
            return;
        }

        FarmManager.Instance._goldText.text = PlayerData.Gold.ToString();
    }

    #endregion
}
