using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerPetData
{
    public string PetName = "No-Name";
    public int PetNumber = -1;
    public bool IsGetted = false;
    public string Position = "No-data";
    public int HP = -1;
    public int ATK = -1;
    public int DEF = -1;
    public int SkillCost = -1;
    public float Cooltime = -1f;
    public float Range = -1;
    public int Level = -1;

    public PlayerPetData(string petName, int petNum, bool getted, 
        string position, int hp, int atk, int def, int cost, float cool, float range, int level)
    {
        PetName = petName;
        PetNumber = petNum;
        IsGetted = getted;
        Position = position;
        HP = hp;
        ATK = atk;
        DEF = def;
        SkillCost = cost;
        Cooltime = cool;
        Range = range;
        Level = level;
    }
}
