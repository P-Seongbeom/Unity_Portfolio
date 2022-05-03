using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSystem : MonoBehaviour
{
    public static PopupSystem Instance;

    public GameObject Popup;

    public Text DifficultyText;
    public Text EnemyText;
    public Text CompensationText;

    private string _difficulty;
    private string _enemy;
    private string _compensation;

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

        _difficulty = DifficultyText.text;
        _enemy = EnemyText.text;
        _compensation = CompensationText.text;
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

    public void OpenPopup(string difficulty, string enemy, string compensation, Action onClickOkay, Action onClickCancel)
    {
        DifficultyText.text += difficulty;
        EnemyText.text += enemy;
        CompensationText.text += compensation;
        _onClickOkay = onClickOkay;
        _onClickCancel = onClickCancel;
        Popup.SetActive(true);
    }

    public void OnClickOkay()
    {
        if (_onClickOkay != null)
        {
            _onClickOkay();
            RevertText();
        }
    }

    public void OnClickCancel()
    {
        if(_onClickCancel != null)
        {
            _onClickCancel();
            RevertText();
        }
    }

    public void ClosePopup()
    {
        _animator.SetTrigger("close");
    }

    public void RevertText()
    {
         DifficultyText.text = _difficulty;
         EnemyText.text = _enemy;
         CompensationText.text = _compensation;
    }
}
