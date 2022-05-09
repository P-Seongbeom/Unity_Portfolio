using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public static StartScreen Instance;

    public string FarmSceneName;

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
        if(Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(FarmSceneName);
        }
    }

    void GoToFarm()
    {
        SceneManager.LoadScene(FarmSceneName);
    }
}