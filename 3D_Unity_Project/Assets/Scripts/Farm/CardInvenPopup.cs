using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CardInvenPopup : FarmPopup
{
    public static CardInvenPopup Instance;

    public GameObject[] InvenSlot;

    protected override void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        base.Awake();
    }

    public void OpenCardInven(List<GameObject> spawnPet, Action onClickClose)
    {
        for(int i = 0; i < InvenSlot.Length; ++i)
        {
            InvenSlot[i].SetActive(i < spawnPet.Count);
            InvenSlot[i].GetComponent<Image>().sprite = i < spawnPet.Count
                                                          ? spawnPet[i].GetComponent<PetInfo>().CardPortrait
                                                          : InvenSlot[i].GetComponent<Image>().sprite;

        }

        _onClickClose = onClickClose;

        Popup.SetActive(true);
    }

    public void OnClickClose()
    {
        if (_onClickClose != null)
        {
            _onClickClose();
        }
    }
}
