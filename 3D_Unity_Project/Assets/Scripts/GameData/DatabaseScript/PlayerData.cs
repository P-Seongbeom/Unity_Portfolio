using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string PlayerName;
    public int Gold;

    public PlayerData(string name, int gold)
    {
        PlayerName = name;
        Gold = gold;
    }
}
