using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance;

        public List<Transform> CheckPoitns;
        public Transform Focus;
        
        public float MoveSpeed = 10f;

        public CinemachineVirtualCamera _vcam;

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
        }

        private void Start()
        {
            _vcam = GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            MoveFocus();
        }

        public void MoveFocus()
        {
            for(int i = 0; i < BattleManager.Instance.InBattlePlayerPets.Count; ++i)
            {
                if (BattleManager.Instance.InBattlePlayerPets[i].transform.position.z > Focus.position.z - 10f)
                {
                    Focus.position = new Vector3(Focus.position.x, Focus.position.y, BattleManager.Instance.InBattlePlayerPets[i].transform.position.z + 10);
                }
            }

            _vcam.Follow = Focus;
        }

    }
}

