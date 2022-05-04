using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public class StageInfo
//{
//    public string StageName = "No-Name";
//    public int StageNumber = -1;
//    public bool OpenStage = false;
//    public int GoldReward = -1;
//    public List<int> CardReward = null;

//    //public string StageName;
//    //public int StageNumber;
//    //public bool OpenStage;
//    //public int GoldReward;
//    //public List<int> CardReward;

//}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public TextAsset StageDatabase;

    //public StageInfo StageData;
    public List<StageData> AllStageList;
    public List<StageData> OpenStageList;

    //public List<Stage> StageData;

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

            AllStageList.Add(new StageData(row[0], int.Parse(row[1]), int.Parse(row[2]), bool.Parse(row[3])));
        }

    }


    void Update()
    {
        if(Input.GetKeyDown("q"))
        {

        }
    }

    public void SaveData()
    {

    }
    
    public void LoadData()
    {

    }

    public void SetupStageData()
    {

    }

    public void OpenStage(int stageNum)
    {
        
    }

    public void CheckNewPlayer()
    {

    }


}
