using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CardInvenPopup : FarmPopup
{
    public static CardInvenPopup Instance;

    public GameObject[] InvenSlot;

    public Text[] LevelText;

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
            if(InvenSlot[i].activeSelf)
            {
                LevelText[i].text = $"Lv : {spawnPet[i].GetComponent<PetInfo>().Level}";
            }

        }

        _onClickClose = onClickClose;

        Popups[0].SetActive(true);
    }

    public void OnClickClose()
    {
        if (_onClickClose != null)
        {
            _onClickClose();
        }
    }
}
