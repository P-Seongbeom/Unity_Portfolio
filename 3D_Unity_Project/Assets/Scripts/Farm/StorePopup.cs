using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class StorePopup : FarmPopup
{
    public static StorePopup Instance;

    Dictionary<int,int> AllPetCards;
    private int _totalWeight;
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

        AllPetCards = new Dictionary<int, int>();

        base.Awake();
    }

    protected void Start()
    {
        for(int i = 0; i < DataManager.Instance.MyPetData.AllPlayerPet.Count; ++i)
        {
            AllPetCards.Add(i, DataManager.Instance.MyPetData.AllPlayerPet[i].Weight);
        }

        for(int i = 0; i < AllPetCards.Count; ++i)
        {
            _totalWeight += AllPetCards[i];
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

                    PickNums.Add(RandomPick());
                    break;
                }
            case 1:
                {
                    PickNums.Clear();

                    for (int i = 0; i < 10; ++i)
                    {
                        PickNums.Add(RandomPick());
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

    int RandomPick()
    {
        int weight = 0;
        int selectNum;

        selectNum = Mathf.RoundToInt(_totalWeight * Random.Range(0.0f, 1.0f));

        for (int i = 0; i < AllPetCards.Count; ++i)
        {
            weight += AllPetCards[i];

            if (selectNum <= weight)
            {
                return i;
            }
        }
        return -1;
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
