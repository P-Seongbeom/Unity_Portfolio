using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class StorePopup : FarmPopup
{
    public static StorePopup Instance;

    public List<int> CardNums;
    public List<int> PickNums;

    public GameObject[] PickedCardSlot;

    private int SelectedSlotNum;

    Action _closeConfimBox;
    Action _closePickBox;

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

    protected void Start()
    {
        for(int i = 0; i < DataManager.Instance.MyPetData.AllPlayerPet.Count; ++i)
        {
            CardNums.Add(i);
        }
    }

    public void OpenStorePopup(Action onClickClose)
    {
        _onClickClose = onClickClose;

        Popups[0].SetActive(true);
    }

    public void ClickGoods(int slotNum)
    {
        SelectedSlotNum = slotNum;
        OpenConfirmPopup(() => { Instance.ClosePopup(Animator[1]); });
    }

    public void OpenConfirmPopup(Action onClickClose)
    {
        _closeConfimBox = onClickClose;

        Popups[1].SetActive(true);
    }

    public void OnClickOkayCard()
    {
        RandomCard(() => { Instance.ClosePopup(Animator[2]); });
    }

    public void RandomCard(Action onClickClose)
    {
        if(DataManager.Instance.PlayerData.Player.Gold < 100)
        {
            return;
        }
        DataManager.Instance.PlayerData.GetGold(-100);

        switch (SelectedSlotNum)
        {
            case 0:
                {
                    PickNums.Clear();

                    PickNums.Add(Random.Range(0, CardNums.Count));
                    break;
                }
            case 1:
                {
                    PickNums.Clear();

                    for (int i = 0; i < 10; ++i)
                    {
                        PickNums.Add(Random.Range(0, CardNums.Count));
                    }
                    break;
                }
        }

        for(int i = 0; i < PickNums.Count; ++i)
        {
            DataManager.Instance.MyPetData.GetPetCard(PickNums[i]);

            foreach(GameObject pet in GameManager.Instance.PetPrefabs)
            {
                if(PickNums[i] == pet.GetComponent<PetInfo>().PetNumber)
                {
                    PickedCardSlot[i].GetComponent<Image>().sprite = pet.GetComponent<PetInfo>().CardPortrait;
                }
            }
        }

        _closePickBox = onClickClose;

        Popups[2].SetActive(true);
    }

    public void OnClickClose()
    {
        if (_onClickClose != null)
        {
            _onClickClose();
        }
    }

    public void CloseComfimBox()
    {
        if(_closeConfimBox != null)
        {
            _closeConfimBox();
        }
    }

    public void ClosePickBox()
    {
        if(_closePickBox != null)
        {
            _closePickBox();
        }
    }

}
