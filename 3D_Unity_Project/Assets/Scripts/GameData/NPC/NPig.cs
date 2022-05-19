using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPig : NPCData
{
    public override void Interact()
    {
        FarmManager.Instance.Communicate(gameObject);
    }
}
