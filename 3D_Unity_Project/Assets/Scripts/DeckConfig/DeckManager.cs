using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;

    //public 

    public string BattleSceneName;

    public void TapInvenCard()
    {
        //EventSystem.current.currentSelectedGameObject;
    }

    public void EnterBattle()
    {
        SceneManager.LoadScene(BattleSceneName);
    }
}
