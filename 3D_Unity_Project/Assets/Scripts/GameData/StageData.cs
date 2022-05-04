using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class StageData
{
    public string StageName = "No-Name";
    public int StageNumber = -1;
    public int GoldReward = -1;
    public bool OpenStage = false;

    public StageData(string stageName, int stageNum, int goldReward, bool openStage )
    {
        StageName = stageName;
        StageNumber = stageNum;
        GoldReward = goldReward;
        OpenStage = openStage;
    }

    //public List<int> CardReward = null;
}

//public class StageData : MonoBehaviour
//{
//    public List<StageInfo> StageInfos;

//    private void Start()
//    {
//        //JsonUtility.
//        //StageInfos.Add
//    }
//}

//public class Stage11 : StageData
//{
//    public Stage11()
//    {
//        StageName = "Stage 1-1";
//        StageNumber = 0;
//        OpenStage = false;
//    }
//}

//public class Stage12 : StageData
//{
//    public Stage12()
//    {
//        StageName = "Stage 1-2";
//        StageNumber = 1;
//        OpenStage = false;
//    }
//}

