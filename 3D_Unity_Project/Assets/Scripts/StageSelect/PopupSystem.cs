using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSystem : MonoBehaviour
{
    public static PopupSystem Instance;

    public GameObject Popup;

    public Text TitleText;
    public Text ContentText;

    private Action _onClickOkay;
    private Action _onClickCancel;

    private Animator _animator;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _animator = Popup.GetComponent<Animator>();
    }

    void Update()
    {
        if(Popup.activeSelf && _animator.GetCurrentAnimatorStateInfo(0).IsName("Close"))
        {
            if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Popup.SetActive(false);
            }
        }
    }

    public void OpenPopup(string title, string content, Action onClickOkay, Action onClickCancel)
    {
        TitleText.text = title;
        ContentText.text = content;
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

        ClosePopup();
    }

    public void OnClickCancel()
    {
        if(_onClickCancel != null)
        {
            _onClickCancel();
        }

        ClosePopup();
    }

    void ClosePopup()
    {
        _animator.SetTrigger("close");
    }
}
