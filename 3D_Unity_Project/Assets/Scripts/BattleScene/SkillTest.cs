using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTest : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public void Button()
    {
        slider.value -= 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
