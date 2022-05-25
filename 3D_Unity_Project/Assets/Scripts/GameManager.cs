using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> PetPrefabs;
    public List<GameObject> HavePets;
    public List<GameObject> AgentPets;

    public List<GameObject> EnemyPrefabs;

    public int CurrentStageNum = -1;

    private void Awake()
    {
        if(null == Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

    }

    void Start()
    {
        if(DataManager.Instance.MyPetData.HavePlayerPet.Count == 0)
        {
            DataManager.Instance.MyPetData.GetPetCard(0);
        }

        HavingPetUpdate();

        for (int i = 0; i < EnemyPrefabs.Count; ++i)
        {
            for (int j = 0; j < DataManager.Instance.EnemyData.EnemyPet.Count; ++j)
            {
                if (EnemyPrefabs[i].name == DataManager.Instance.EnemyData.EnemyPet[j].PetName)
                {
                    EnemyPrefabs[i].GetComponent<EPetInfo>().SetInfo(
                        DataManager.Instance.EnemyData.EnemyPet[j].PetName,
                        DataManager.Instance.EnemyData.EnemyPet[j].PetNumber,
                        DataManager.Instance.EnemyData.EnemyPet[j].HP,
                        DataManager.Instance.EnemyData.EnemyPet[j].ATK,
                        DataManager.Instance.EnemyData.EnemyPet[j].DEF,
                        DataManager.Instance.EnemyData.EnemyPet[j].Cooltime,
                        DataManager.Instance.EnemyData.EnemyPet[j].AttackRange,
                        DataManager.Instance.EnemyData.EnemyPet[j].SkillRange);
                }
            }
        }
    }

    public void HavingPetUpdate()
    {
        for (int i = 0; i < PetPrefabs.Count; ++i)
        {
            for(int j = 0; j < DataManager.Instance.MyPetData.AllPlayerPet.Count; ++j)
            {
                if(PetPrefabs[i].name == DataManager.Instance.MyPetData.AllPlayerPet[j].PetName)
                {
                    PetPrefabs[i].GetComponent<PetInfo>().SetInfo(
                        DataManager.Instance.MyPetData.AllPlayerPet[j].PetName,
                        DataManager.Instance.MyPetData.AllPlayerPet[j].PetNumber,
                        DataManager.Instance.MyPetData.AllPlayerPet[j].IsGetted,
                        DataManager.Instance.MyPetData.AllPlayerPet[j].Position,
                        DataManager.Instance.MyPetData.AllPlayerPet[j].HP,
                        DataManager.Instance.MyPetData.AllPlayerPet[j].ATK,
                        DataManager.Instance.MyPetData.AllPlayerPet[j].DEF,
                        DataManager.Instance.MyPetData.AllPlayerPet[j].SkillCost,
                        DataManager.Instance.MyPetData.AllPlayerPet[j].Cooltime,
                        DataManager.Instance.MyPetData.AllPlayerPet[j].AttackRange,
                        DataManager.Instance.MyPetData.AllPlayerPet[j].SkillRange,
                        DataManager.Instance.MyPetData.AllPlayerPet[j].Level);
                }
            }
        }

        for (int i = 0; i < PetPrefabs.Count; ++i)
        {
            for (int j = 0; j < DataManager.Instance.MyPetData.HavePlayerPet.Count; ++j)
            {
                if (PetPrefabs[i].name == DataManager.Instance.MyPetData.HavePlayerPet[j].PetName)
                {
                    PetPrefabs[i].GetComponent<PetInfo>().SetInfo(
                        DataManager.Instance.MyPetData.HavePlayerPet[j].PetName,
                        DataManager.Instance.MyPetData.HavePlayerPet[j].PetNumber,
                        DataManager.Instance.MyPetData.HavePlayerPet[j].IsGetted,
                        DataManager.Instance.MyPetData.HavePlayerPet[j].Position,
                        DataManager.Instance.MyPetData.HavePlayerPet[j].HP,
                        DataManager.Instance.MyPetData.HavePlayerPet[j].ATK,
                        DataManager.Instance.MyPetData.HavePlayerPet[j].DEF,
                        DataManager.Instance.MyPetData.HavePlayerPet[j].SkillCost,
                        DataManager.Instance.MyPetData.HavePlayerPet[j].Cooltime,
                        DataManager.Instance.MyPetData.HavePlayerPet[j].AttackRange,
                        DataManager.Instance.MyPetData.HavePlayerPet[j].SkillRange,
                        DataManager.Instance.MyPetData.HavePlayerPet[j].Level);
                }
            }
        }

        for (int i = 0; i < PetPrefabs.Count; ++i)
        {
            if (PetPrefabs[i].GetComponent<PetInfo>().IsGetted)
            {
                if(false == HavePets.Contains(PetPrefabs[i]))
                {
                    HavePets.Add(PetPrefabs[i]);
                }
            }
        }
    }

    public void LevelUp()
    {

    }
}
