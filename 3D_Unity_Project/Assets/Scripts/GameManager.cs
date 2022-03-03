using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public InputManager InputHandle;


    private void Awake()
    {
        GameManager.Instance = this;
        
    }
    void Start()
    {
        PlayerPetController controller = GetComponent<PlayerPetController>();
        InputHandle.AddController(controller);
    }

    
    void Update()
    {
        
    }
}
