using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetInfo : MonoBehaviour
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
    public float Level = -1;

    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInfo(string petName, int petNum, bool getted,
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
