using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStageInfo : MonoBehaviour
{
    public Button Button;

    public StageData StageData;

    private void Awake()
    {
        Button = gameObject.GetComponent<Button>();
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        CheckOpenStage();
    }

    public void CheckOpenStage()
    {
        if(StageData.OpenStage)
        {
            Button.interactable = true;
        }
        else
        {
            Button.interactable = false;
        }
    }
}
