using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NGoat : NPCData
{
    private void Start()
    {
        LeaderPet = FarmManager.Instance.SpawnedPets[0].transform;
    }

    private void Update()
    {
        _distanceToPlayer = (transform.position - LeaderPet.transform.position).magnitude;

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
