using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene
{
    public class CameraFocus : MonoBehaviour
    {
        public static CameraFocus Instance;

        public List<Transform> CheckPoitns;

        public float MoveSpeed = 10f;
        public bool MoveCamera = true;

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
            MoveCamera = false;
        }

        private void Update()
        {
            if(MoveCamera)
            {
                MoveFocus();
            }

            ActivateCheckPoint();
        }

        public void MoveFocus()
        {
            this.transform.Translate(new Vector3(0, 0, MoveSpeed * Time.deltaTime));
        }

        public void ActivateCheckPoint()
        {
            foreach(Transform point in CheckPoitns)
            {
                if(point.gameObject.activeSelf && transform.position.z >= point.position.z)
                {
                    MoveCamera = false;
                    point.gameObject.SetActive(false);
                    StopCoroutine("MovePet");
                }
            }
        }
    }
}

