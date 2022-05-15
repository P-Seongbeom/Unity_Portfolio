using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPetData
{
    public string PetName = "No-Name";
    public int PetNumber = -1;
    public int HP = -1;
    public int ATK = -1;
    public int DEF = -1;
    public float Cooltime = -1f;
    public float AttackRange = -1;
    public float SkillRange = -1;

    public EnemyPetData(string petName, int petNum, 
        int hp, int atk, int def, float cool, float atkRange, float sklRange)
    {
        PetName = petName;
        PetNumber = petNum;
        HP = hp;
        ATK = atk;
        DEF = def;
        Cooltime = cool;
        AttackRange = atkRange;
        SkillRange = sklRange;
    }
}
