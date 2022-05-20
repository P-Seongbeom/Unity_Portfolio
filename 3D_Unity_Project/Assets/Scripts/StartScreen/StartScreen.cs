using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public static StartScreen Instance;

    public GameObject NameBox;
    public Text NewName;

    public string FarmSceneName;

    private bool Started = false;

    private void Awake()
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

    void Update()
    {
        if(Input.GetMouseButtonUp(0) && false == Started)
        {
            StartGame();
        }
    }

    void MakeNameBox()
    {
        NameBox.SetActive(true);
    }

    void StartGame()
    {
        if(DataManager.Instance.PlayerData.Player.PlayerName == "No-Name")
        {
            MakeNameBox();
        }
        else
        {
            SceneManager.LoadScene(FarmSceneName);
        }
    }

    public void GoToFarm()
    {
        DataManager.Instance.PlayerData.SetPlayer(NewName.text, 0);

        SceneManager.LoadScene(FarmSceneName);
    }
}
