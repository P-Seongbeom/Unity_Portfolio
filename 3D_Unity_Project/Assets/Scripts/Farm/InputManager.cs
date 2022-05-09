using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    private Camera _camera;
    public Transform Destination;

    public List<PetController> Controllers;

    private RaycastHit[] _hits;
    private Ray _ray;
    private float _closestDistance;
    private Vector3 _destination;

    [SerializeField]
    private float _stopDistance = 5f;

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

        foreach (PetController controller in Controllers)
        {
            controller.Agent.stoppingDistance = _stopDistance;
        }
    }

    private void SetDestinationPoint()
    {
        if(Input.GetMouseButtonDown(0) && FarmManager.Instance.talkIndex > 0)
        {
            FarmManager.Instance.Communicate(FarmManager.Instance.ScanObject);
            return;
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            _closestDistance = 0;
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            _hits = Physics.RaycastAll(_ray);

            for(int i = 0; i < _hits.Length; ++i)
            {
                if(_hits[i].collider.tag == "Ground" && !FarmManager.Instance.isTalk)
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
                //NPC 상호작용 부분
                else if(_hits[i].collider.tag == "NPC")
                {
                    if(_hits[i].collider.gameObject.GetComponent<NPCData>().CanInteract)
                    {
                        FarmManager.Instance.Communicate(_hits[i].collider.gameObject);
                    }
                    //return;
                }
            }

            
            Destination.position = _destination;

            foreach(PetController controller in Controllers)
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
        PetController[] temp = FindObjectsOfType<PetController>();
        foreach(PetController controller in temp)
        {
            this.AddController(controller);
        }
    }

    public void AddController(PetController control)
    {
        control.SetDestination(Destination.position);
        if(false == Controllers.Contains(control))
        {
            Controllers.Add(control);
        }
    }


}
