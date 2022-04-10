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
                "\n���ĳ�!!!!!!!!!!!!!!!!!!\n�����������!!!!!!!!!!!!!!!!!!!!!!!!!!!!"
                ,()=> { Debug.Log("�����!!!"); },
                ()=> { Debug.Log("�߿�...."); });
        }

        public void BackToFarm()
        {
            SceneManager.LoadScene(FarmSceneName);
        }
    }
}