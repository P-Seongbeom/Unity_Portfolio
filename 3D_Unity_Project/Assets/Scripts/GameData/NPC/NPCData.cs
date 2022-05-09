using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCData : MonoBehaviour
{
    public TalkData _dialogueData;

    public Transform LeaderPet;

    public Sprite Portrait;

    public int NpcId;

    public float InteractRange;
    public float _distanceToPlayer;

    public bool CanInteract;
}
