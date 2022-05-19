using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FarmPopup : MonoBehaviour
{
    public List<GameObject> Popups;

    public List<Animator> Animator;

    protected Action _onClickClose;

    protected virtual void Awake()
    {
        for(int i = 0; i < Animator.Count; ++i)
        {
            Animator[0] = Popups[0].GetComponent<Animator>();
        }
    }

    protected virtual void Update()
    {
        for(int i = 0; i < Popups.Count; ++i)
        {
            if (Popups[i].activeSelf && Animator[i].GetCurrentAnimatorStateInfo(0).IsName("Close"))
            {
                if (Animator[i].GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    Popups[i].SetActive(false);
                }
            }
        }
    }

    public void ClosePopup(Animator animator)
    {
        animator.SetTrigger("close");
    }
}
