using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;

    public string StageSelectSceneName;
    public string BattleSceneName;

    public List<GameObject> CurrentHavePets;

    public GameObject[] InvenSlot;
    public GameObject[] SelectedSlot;

    private int _clickCount;

    public Sprite nothing;

    private void Awake()
    {
        CurrentHavePets.AddRange(GameManager.Instance.HavePets);

        for (int i = 0; i < InvenSlot.Length; ++i)
        {
            InvenSlot[i].SetActive(i < CurrentHavePets.Count);
            InvenSlot[i].GetComponent<Image>().sprite = i < CurrentHavePets.Count 
                                                      ? CurrentHavePets[i].GetComponent<PetInfo>().CardPortrait 
                                                      : InvenSlot[i].GetComponent<Image>().sprite;
        }
    }

    public void TapInvenSlot(int slotNum)
    {
        if(InvenSlot[slotNum].GetComponent<Image>().color == Color.white && _clickCount < CurrentHavePets.Count)
        {
            ChangeFrameColor(slotNum, Color.grey);
            ApplyToSelectSlot(slotNum);
            ++_clickCount;
        }
        else if(_clickCount == CurrentHavePets.Count || _clickCount > 0)
        {
            ChangeFrameColor(slotNum, Color.white);
            CancelApply(slotNum);
            --_clickCount;
        }
    }

    public void EnterBattle()
    {
        for(int i = 0; i < SelectedSlot.Length; ++i)
        {
            for(int j = 0; j < GameManager.Instance.HavePets.Count; ++j)
            {
                if(SelectedSlot[i].GetComponent<Image>().sprite == GameManager.Instance.HavePets[j].GetComponent<PetInfo>().CardPortrait)
                {
                    GameManager.Instance.AgentPets.Add(GameManager.Instance.HavePets[j]);
                }
            }
        }

        SceneManager.LoadScene(BattleSceneName);
    }

    public void BackToStageSelect()
    {
        SceneManager.LoadScene(StageSelectSceneName);
    }

    public void ChangeFrameColor(int slotNum, Color color)
    {
        InvenSlot[slotNum].transform.transform.GetComponent<Image>().color = color;
    }

    public void ApplyToSelectSlot(int tapSlotNum)
    {
        for (int i = 0; i < InvenSlot.Length; ++i)
        {
            for (int j = 0; j < SelectedSlot.Length; ++j)
            {
                if (SelectedSlot[j].GetComponent<Image>().sprite == nothing)
                {
                    SelectedSlot[j].GetComponent<Image>().sprite = InvenSlot[tapSlotNum].GetComponent<Image>().sprite;
                    return;
                }
            }
        }
    }

    public void CancelApply(int tapSlotNum)
    {
        for (int i = 0; i < InvenSlot.Length; ++i)
        {
            for(int j = 0; j < SelectedSlot.Length; ++j)
            {
                if(InvenSlot[tapSlotNum].GetComponent<Image>().sprite == SelectedSlot[j].GetComponent<Image>().sprite)
                {
                    SelectedSlot[j].GetComponent<Image>().sprite = nothing;
                }
            }
        }
    }
}
