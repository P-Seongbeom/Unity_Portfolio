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
        if(DataManager.Instance.HavePlayerPet.Count == 0)
        {
            DataManager.Instance.GetPetCard(0);
        }

        for (int i = 0; i < PetPrefabs.Count; ++i)
        {
            for(int j = 0; j < DataManager.Instance.AllPlayerPet.Count; ++j)
            {
                if(PetPrefabs[i].name == DataManager.Instance.AllPlayerPet[j].PetName)
                PetPrefabs[i].GetComponent<PetInfo>().SetInfo(
                    DataManager.Instance.AllPlayerPet[i].PetName,
                    DataManager.Instance.AllPlayerPet[i].PetNumber,
                    DataManager.Instance.AllPlayerPet[i].IsGetted,
                    DataManager.Instance.AllPlayerPet[i].Position,
                    DataManager.Instance.AllPlayerPet[i].HP,
                    DataManager.Instance.AllPlayerPet[i].ATK,
                    DataManager.Instance.AllPlayerPet[i].DEF,
                    DataManager.Instance.AllPlayerPet[i].SkillCost,
                    DataManager.Instance.AllPlayerPet[i].Cooltime,
                    DataManager.Instance.AllPlayerPet[i].AttackRange,
                    DataManager.Instance.AllPlayerPet[i].SkillRange,
                    DataManager.Instance.AllPlayerPet[i].Level);
            }
        }

        for (int i = 0; i < PetPrefabs.Count; ++i)
        {
            for(int j = 0; j < DataManager.Instance.HavePlayerPet.Count; ++j)
            {
                if(PetPrefabs[i].name == DataManager.Instance.HavePlayerPet[j].PetName)
                {
                    PetPrefabs[i].GetComponent<PetInfo>().SetInfo(
                        DataManager.Instance.HavePlayerPet[j].PetName,
                        DataManager.Instance.HavePlayerPet[j].PetNumber,
                        DataManager.Instance.HavePlayerPet[j].IsGetted,
                        DataManager.Instance.HavePlayerPet[j].Position,
                        DataManager.Instance.HavePlayerPet[j].HP,
                        DataManager.Instance.HavePlayerPet[j].ATK,
                        DataManager.Instance.HavePlayerPet[j].DEF,
                        DataManager.Instance.HavePlayerPet[j].SkillCost,
                        DataManager.Instance.HavePlayerPet[j].Cooltime,
                        DataManager.Instance.HavePlayerPet[j].AttackRange,
                        DataManager.Instance.HavePlayerPet[j].SkillRange,
                        DataManager.Instance.HavePlayerPet[j].Level);
                }
            }
        }

        for(int i = 0; i < PetPrefabs.Count; ++i)
        {
            if (PetPrefabs[i].GetComponent<PetInfo>().IsGetted)
            {
                HavePets.Add(PetPrefabs[i]);
            }
        }

        for (int i = 0; i < EnemyPrefabs.Count; ++i)
        {
            for (int j = 0; j < DataManager.Instance.EnemyPet.Count; ++j)
            {
                if (EnemyPrefabs[i].name == DataManager.Instance.EnemyPet[j].PetName)
                {
                    EnemyPrefabs[i].GetComponent<EPetInfo>().SetInfo(
                        DataManager.Instance.EnemyPet[j].PetName,
                        DataManager.Instance.EnemyPet[j].PetNumber,
                        DataManager.Instance.EnemyPet[j].HP,
                        DataManager.Instance.EnemyPet[j].ATK,
                        DataManager.Instance.EnemyPet[j].DEF,
                        DataManager.Instance.EnemyPet[j].Cooltime,
                        DataManager.Instance.EnemyPet[j].AttackRange,
                        DataManager.Instance.EnemyPet[j].SkillRange);
                }
            }
        }
    }
}
