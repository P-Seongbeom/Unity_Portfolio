using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Click : MonoBehaviour
{
    private Camera _camera;
    private Animator _animator;
    private NavMeshAgent _agent;

    private bool _isMove;
    private Vector3 _destination;

    void Awake()
    {
        _camera = Camera.main;
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                SetDestination(hit.point);
            }
        }

        LookMoveDirection();
    }

    void SetDestination(Vector3 dest)
    {
        _agent.SetDestination(dest);
        _destination = dest;
        _isMove = true;
        _animator.SetBool("_isMove", true);
    }

    void LookMoveDirection()
    {
        if(_isMove)
        {
            if(_agent.velocity.magnitude == 0f)
            {
                _isMove = false;
                _animator.SetBool("_isMove", false);
                return;
            }

            var dir = new Vector3(_agent.steeringTarget.x, transform.position.y, _agent.steeringTarget.z) - transform.position;
            transform.forward = dir;
        }
    }
}
