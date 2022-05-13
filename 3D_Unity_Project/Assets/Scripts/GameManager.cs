using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> PetPrefabs;
    public List<GameObject> HavePets;
    public List<GameObject> AgentPets;

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

        for(int i = 0; i < PetPrefabs.Count; ++i)
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
                        DataManager.Instance.HavePlayerPet[j].Range,
                        DataManager.Instance.HavePlayerPet[j].Level);
                }
            }

            if (PetPrefabs[i] == null)
            {
                PetPrefabs.Remove(PetPrefabs[i]);
            }
        }
        for(int i = 0; i < PetPrefabs.Count; ++i)
        {
            if (PetPrefabs[i].GetComponent<PetInfo>().IsGetted)
            {
                HavePets.Add(PetPrefabs[i]);
            }
        }
    }
}
