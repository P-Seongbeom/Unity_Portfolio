using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Farm
{
    public class FarmManager : MonoBehaviour
    {
        public static FarmManager Instance;
        public InputManager InputHandle;

        public List<GameObject> PetPrefabs;
        public List<GameObject> SpawnedPets;

        public string StageSelectScene;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            foreach (GameObject pet in PetPrefabs)
            {
                if (pet == null)
                {
                    PetPrefabs.Remove(pet);
                }
            }

            foreach (GameObject pet in PetPrefabs)
            {
                Spawn(pet);
            }
        }

        public void Spawn(GameObject prefab)
        {
            if(prefab.GetComponent<PetController>() == null)
            {
                Debug.LogError("Prefab doesn't have 'PlayerPetController' component.");
            }
            else
            {
                GameObject obj = Instantiate(prefab);
                obj.transform.position = new Vector3(-2, 2, -2);
                PetController controller = obj.GetComponent<PetController>();
                InputHandle.AddController(controller);
                SpawnedPets.Add(controller.gameObject);
            }
        }

        public void EnterStageSelect()
        {
            SceneManager.LoadScene(StageSelectScene);
        }
    }

}
