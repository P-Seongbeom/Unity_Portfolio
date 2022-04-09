using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    private CinemachineVirtualCamera _vcam;

    void Start()
    {
        _vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        _vcam.Follow = GameManager.Instance.SpawnedPets[0].transform;
    }
}
