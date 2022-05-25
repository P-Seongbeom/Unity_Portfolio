using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThresholdControl : MonoBehaviour
{
    private float inchToCm = 2.54f;

    [SerializeField]
    private EventSystem eventSystem = null;

    [SerializeField]
    private float dragThresholdCm = 0.5f;

    private void Awake()
    {
        if(eventSystem == null)
        {
            eventSystem = GetComponent<EventSystem>();
        }

        SetDragThreshold();
    }

    private void SetDragThreshold()
    {
        if(eventSystem != null)
        {
            eventSystem.pixelDragThreshold = (int)(dragThresholdCm * Screen.dpi / inchToCm);
        }
    }
}
