using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Camera _camera;
    public Transform Destination;

    public List<PlayerPetController> Controllers;

    private RaycastHit[] _hits;
    private Ray _ray;
    private float _closestDistance;
    private Vector3 _destination;

    void Awake()
    {
        _camera = Camera.main;
    }

    void Start()
    {
        if(Destination == null)
        {
            Debug.LogWarning("Destination not set.");
            GameObject obj = new GameObject("Destination");
            Destination = obj.transform;
        }
        if(Controllers.Count == 0)
        {
            Debug.LogWarning("PlayerPetController is empty.");
        }

        this.AddAllController();
    }

    void Update()
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
            Destination.position = _destination;
            foreach(PlayerPetController controller in Controllers)
            {
                controller.SetDestination(_destination);
            }

        }
    }

    private void RenewPoint(RaycastHit[] hits, int i)
    {
        _closestDistance = Vector3.Distance(_camera.transform.position, hits[i].point);
        _destination = hits[i].point;
    }

    public void AddAllController()
    {
        PlayerPetController[] temp = FindObjectsOfType<PlayerPetController>();
        foreach(PlayerPetController controller in temp)
        {
            this.AddController(controller);
        }
    }

    public void AddController(PlayerPetController control)
    {
        control.SetDestination(Destination.position);
        if(false == Controllers.Contains(control))
        {
            Controllers.Add(control);
        }
    }
}
