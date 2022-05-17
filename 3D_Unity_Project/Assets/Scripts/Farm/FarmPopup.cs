using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FarmPopup : MonoBehaviour
{
    public GameObject Popup;

    public Animator Animator;

    protected Action _onClickClose;

    protected virtual void Awake()
    {
        Animator = Popup.GetComponent<Animator>();
    }

    protected void Update()
    {
        if (Popup.activeSelf && (Animator.GetCurrentAnimatorStateInfo(0).IsName("Close") 
                                || Animator.GetCurrentAnimatorStateInfo(0).IsName("CardClose")))
        {
            if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Popup.SetActive(false);
            }
        }
    }

    public void ClosePopup()
    {
        Animator.SetTrigger("close");
    }
}
