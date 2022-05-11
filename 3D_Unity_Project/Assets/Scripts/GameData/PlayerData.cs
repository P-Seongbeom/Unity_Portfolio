using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string PlayerName;
    public int PlayerNum;
    public int Gold;

    public PlayerData(string name, int num, int gold)
    {
        PlayerName = name;
        PlayerNum = num;
        Gold = gold;
    }
}
