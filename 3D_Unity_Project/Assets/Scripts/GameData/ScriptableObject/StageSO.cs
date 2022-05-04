using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stage
{
    public string StageName = "No-Name";
    public int StageNumber = -1;
    public bool OpenStage = false;
    public int GoldReward = -1;
    public List<int> CardReward = null;
}

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

[CreateAssetMenu(fileName = "StageSO", menuName = "Scriptable Object/StageSO")]
public class StageSO : ScriptableObject
{
    public Stage[] StageDatas;
}

