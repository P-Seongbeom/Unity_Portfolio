using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NGoat : NPCData
{
    public override void Interact()
    {
        //상점창 열기
        StorePopup.Instance.OpenStorePopup(() => { StorePopup.Instance.ClosePopup(StorePopup.Instance.Animator[0]); });
    }
}
