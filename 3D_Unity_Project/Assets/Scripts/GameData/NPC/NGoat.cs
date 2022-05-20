using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NGoat : NPCData
{
    public override void Interact()
    {
        StorePopup.Instance.OpenStorePopup(() => { StorePopup.Instance.ClosePopup(StorePopup.Instance.Animator[0]);
                                                   FarmManager.Instance.BgmPlayer.PlayBGM("basic"); });
        FarmManager.Instance.BgmPlayer.PlayBGM("store");
    }
}
