using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public StageDatabase StageData;
    public PlayerPetDatabase MyPetData;
    public EnemyPetDatabase EnemyData;
    public QuestLogData QuestLog;
    public PlayerDatabase PlayerData;

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

        StageData = new StageDatabase();
        MyPetData = new PlayerPetDatabase();
        EnemyData = new EnemyPetDatabase();
        QuestLog = new QuestLogData();
        PlayerData = new PlayerDatabase();

        StageData.InitData();
        MyPetData.InitData();
        EnemyData.InitData();
        QuestLog.InitData();
        PlayerData.InitData();
    }

    void Update()
    {
    }

    public void ResetAllData()
    {
        File.Delete(StageData.path);
        File.Delete(MyPetData.path);
        File.Delete(EnemyData.path);
        File.Delete(QuestLog.path);
        File.Delete(PlayerData.path);

        Destroy(this);
        Destroy(GameManager.Instance);
        Destroy(QuestManager.Instance);

        SceneManager.LoadScene(0);
    }
}
