using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class StageSelectPopup : MonoBehaviour
{
    public static StageSelectPopup Instance;

    public GameObject Popup;

    public Animator Animator;

    public Text RewardText;

    private string _reward;

    private Action _onClickOkay;
    private Action _onClickCancel;

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

        _reward = RewardText.text;

        Animator = Popup.GetComponent<Animator>();
    }

    void Update()
    {
        if (Popup.activeSelf && Animator.GetCurrentAnimatorStateInfo(0).IsName("Close"))
        {
            if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                RevertText();

                Popup.SetActive(false);
            }
        }
    }

    public void OpenPopup(string compensation, Action onClickOkay, Action onClickCancel)
    {
        RewardText.text += compensation;
        _onClickOkay = onClickOkay;
        _onClickCancel = onClickCancel;

        Popup.SetActive(true);
    }

    public void OnClickOkay()
    {
        if (_onClickOkay != null)
        {
            _onClickOkay();
        }
    }

    public void OnClickCancel()
    {
        if (_onClickCancel != null)
        {
            _onClickCancel();
        }
    }

    public void RevertText()
    {
        RewardText.text = _reward;
    }

    public void ClosePopup()
    {
        Animator.SetTrigger("close");
    }
}
