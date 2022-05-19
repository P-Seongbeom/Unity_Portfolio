using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuestPopup : FarmPopup
{
    public static QuestPopup Instance;

    public Text TitleText;
    public Text ContentText;
    public Text RewardText;

    protected string _reward;

    protected override void Awake()
    {
        base.Awake();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _reward = RewardText.text;
    }

    public void OpenPopup(string title, string content, string reward, Action onClickClose)
    {
        TitleText.text = title;
        ContentText.text = content;
        RewardText.text += reward;

        _onClickClose = onClickClose;

        Popups[0].SetActive(true);
    }

    public void OnClickClose()
    {
        if(_onClickClose != null)
        {
            _onClickClose();
            StartCoroutine(RevertText());
        }
    }

    public IEnumerator RevertText()
    {
        yield return new WaitForSeconds(1f);

        RewardText.text = _reward;
    }
}
