using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerPetController PlayerController;

    private Camera _camera;
    private Transform Destination;


    private RaycastHit[] _hits;
    private Ray _ray;
    private float _closestDistance;
    private Vector3 _destination;
    

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _destination = Vector3.zero;
    }

    private void Update()
    {
        SetDestinationPoint();
    }

    private void SetDestinationPoint()
    {
        if (Input.GetMouseButton(0))
        {
            _closestDistance = 0;
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            _hits = Physics.RaycastAll(_ray);
            
            for(int i = 0; i < _hits.Length; ++i)
            {
                if(_hits[i].collider.tag == "Ground")
                {
                    if(_closestDistance == 0f)
                    {
                        RenewPoint(_hits, i);
                    }
                    else if(Vector3.Distance(_camera.transform.position, _hits[i].point) < _closestDistance)
                    {
                        RenewPoint(_hits, i);
                    }
                }
            }

            PlayerController.SetDestination(_destination);

        }
    }

    private void RenewPoint(RaycastHit[] hits, int i)
    {
        _closestDistance = Vector3.Distance(_camera.transform.position, hits[i].point);
        _destination = hits[i].point;
    }

    public void AddController(PlayerPetController control)
    {
        control.SetDestination(_destination);
    }
}
