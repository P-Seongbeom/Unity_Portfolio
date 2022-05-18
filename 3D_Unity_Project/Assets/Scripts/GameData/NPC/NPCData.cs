using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class NPCData : MonoBehaviour
{
    public TalkData _dialogueData;

    public Transform LeaderPet;

    public Sprite Portrait;

    public int NpcId;

    public float InteractRange;
    public float _distanceToPlayer;

    public bool CanInteract;

    protected virtual void Start()
    {
        if(LeaderPet)
        {
            LeaderPet = FarmManager.Instance.SpawnedPets[0].transform;
        }
    }

    protected virtual void Update()
    {
        if (null == LeaderPet)
        {
            LeaderPet = FarmManager.Instance.SpawnedPets[0].transform;
        }
        else
        {
            _distanceToPlayer = (transform.position - LeaderPet.transform.position).magnitude;
        }

        if (InteractRange >= _distanceToPlayer)
        {
            CanInteract = true;
        }
        else
        {
            CanInteract = false;
        }
    }
}
