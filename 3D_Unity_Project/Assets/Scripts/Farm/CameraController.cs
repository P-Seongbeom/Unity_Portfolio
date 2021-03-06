using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farm
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance;

        private CinemachineVirtualCamera _vcam;

        public Transform Destination;

        void Start()
        {
            _vcam = GetComponent<CinemachineVirtualCamera>();
        }

        void Update()
        {
            _vcam.Follow = FarmManager.Instance.SpawnedPets[0].transform;
        }
    }
}
