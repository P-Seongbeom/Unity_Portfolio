using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FarmUIPopup : MonoBehaviour
{
    public static FarmUIPopup Instance;

    public GameObject Popup;

    public Animator Animator;

    public Text TitleText;
    public Text ContentText;
    public Text RewardText;

    private string _reward;

    private Action _onClickClose;

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
                Popup.SetActive(false);
            }
        }
    }

    public void OpenPopup(string title, string content, string reward, Action onClickClose)
    {
        TitleText.text = title;
        ContentText.text = content;
        RewardText.text += reward;

        _onClickClose = onClickClose;

        Popup.SetActive(true);
    }

    public void OnClickClose()
    {
        if (_onClickClose != null)
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

    public void ClosePopup()
    {
        Animator.SetTrigger("close");
    }
}
