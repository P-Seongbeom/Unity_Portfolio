using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class StageSelectManager : MonoBehaviour
{
    public static StageSelectManager Instance;

    public string FarmSceneName;
    public string DeckConfigSceneName;

    public GameObject SelectedObect;
    public int SelectStageNum;

    public GameObject[] StagePrefabs;
    public GameObject[] StageButtons;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //for(int i = 0; i < DataManager.Instance.AllStageList.Count - 1; ++i)
        //{
        //    StagePrefabs[i].GetComponent<StageInfo>().StageData = DataManager.Instance.AllStageList[i];
        //    StageButtons[i].GetComponent<SelectStageInfo>().StageData = StagePrefabs[i].GetComponent<StageInfo>().StageData;
        //}

        for (int i = 0; i < DataManager.Instance.OpenStageList.Count; ++i)
        {
            StagePrefabs[i].GetComponent<StageInfo>().StageData = DataManager.Instance.OpenStageList[i];
            StageButtons[i].GetComponent<SelectStageInfo>().StageData = StagePrefabs[i].GetComponent<StageInfo>().StageData;
        }
    }
    
    public void OnClickStage()
    {
        SelectedObect = EventSystem.current.currentSelectedGameObject;
        string reward = SelectedObect.GetComponent<SelectStageInfo>().StageData.GoldReward.ToString();
        SelectStageNum = SelectedObect.GetComponent<SelectStageInfo>().StageData.StageNumber;

        StageSelectPopup.Instance.OpenPopup( 
            $"{reward}°ñµå", 
            () => { GoToDeckConfig(); }, 
            () => { StageSelectPopup.Instance.ClosePopup(); });
    }

    public void BackToFarm()
    {
        SceneManager.LoadScene(FarmSceneName);
    }

    public void GoToDeckConfig()
    {
        GameManager.Instance.CurrentStageNum = SelectStageNum;
        SceneManager.LoadScene(DeckConfigSceneName);
    }
}

