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
    public bool ClearStage = false;

    public StageData(string stageName, int stageNum, int goldReward, bool openStage, bool clearStage )
    {
        StageName = stageName;
        StageNumber = stageNum;
        GoldReward = goldReward;
        OpenStage = openStage;
        ClearStage = clearStage;
    }
}

