using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public string FarmSceneName;
    public string BattleSceneName;

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


    }

    public void OnClickButton()
    {
        //PopupSystem.Instance.OpenPopup(
        //    "Title Info",
        //    "\n!!!!!!!!!!!!!!!!!!\n庚岨伸嬢左室推!!!!!!!!!!!!!!!!!!!!!!!!!!!!"
        //    , () => { PopupSystem.Instance.OpenPopup("Title Info", "駅喰!!!", 
        //        () => { Debug.Log("Yes, Go Battle"); 
        //            PopupSystem.Instance.ClosePopup();
        //            GoToBattle();}, 
        //        () => { Debug.Log("No, close"); 
        //            PopupSystem.Instance.ClosePopup(); }); },
        //    () => { PopupSystem.Instance.OpenPopup("Title Info", "馬帖徹展帖 ばばばばばばばば", 
        //        () => { Debug.Log("Yes,close"); 
        //            PopupSystem.Instance.ClosePopup(); }, 
        //        () => { Debug.Log("No,close"); 
        //            PopupSystem.Instance.ClosePopup(); }); });
    }
    
    public void OnClickStage()
    {
        PopupSystem.Instance.OpenPopup(
            "習崇", 
            "壱丞戚, 神軒, 紫戎, 印", 
            "100茨球", 
            () => { GoToBattle(); }, 
            () => { PopupSystem.Instance.ClosePopup(); }
            );
    }

    public void BackToFarm()
    {
        SceneManager.LoadScene(FarmSceneName);
    }

    public void GoToBattle()
    {
        SceneManager.LoadScene(BattleSceneName);
    }
}

