using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StageSelect
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public string FarmSceneName;

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
            PopupSystem.Instance.OpenPopup(
                "Title Info",
                "\n릴파넴!!!!!!!!!!!!!!!!!!\n문좀열어보세요!!!!!!!!!!!!!!!!!!!!!!!!!!!!"
                ,()=> { Debug.Log("릴뱅온!!!"); },
                ()=> { Debug.Log("추워...."); });
        }

        public void BackToFarm()
        {
            SceneManager.LoadScene(FarmSceneName);
        }
    }
}
