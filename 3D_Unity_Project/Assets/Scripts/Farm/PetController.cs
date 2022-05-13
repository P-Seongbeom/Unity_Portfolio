using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetController : MonoBehaviour
{
    public Animator PetAnimator;
    public NavMeshAgent Agent;

    public float JumpPower = 20f;
    public float GravityRatio = 1f;

    private AgentMover _agentMover;

    private float _height;
    private float _baseOffset;
    private float _jumpCooldown = 0;

    private int _moveSpeed;
    private int _jump;

    private Dictionary<int, string> _states;

    Vector3 speed;

    void Start()
    {
        if(_agentMover == null)
        {
            _agentMover = GetComponent<AgentMover>();
        }

        _height = 0;
        _baseOffset = Agent.baseOffset;
        
        _states = new Dictionary<int, string>();
        _states.Add(Animator.StringToHash("Base.Idle"), "Idle");
        _states.Add(Animator.StringToHash("Base.Move"), "Move");
        _states.Add(Animator.StringToHash("Base.Jump"), "Jump");

        _moveSpeed = Animator.StringToHash("moveSpeed");
        _jump = Animator.StringToHash("jump");
    }

    void Update()
    {
        speed = Agent.velocity;

        PetAnimator.SetFloat(_moveSpeed, speed.sqrMagnitude);

        Agent.baseOffset = _height + _baseOffset;

        _jumpCooldown -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "JumpFence" && _jumpCooldown <= 0)
        {
            StartCoroutine(JumpFence(JumpPower));
        }
    }

    private IEnumerator JumpFence(float jumpPower)
    {
        Jump();

        _jumpCooldown = 0.5f;
        _height = 0.05f;

        while(_height > 0f)
        {
            jumpPower = Mathf.Lerp(jumpPower, 0, jumpPower * 0.1f * Time.deltaTime);
            jumpPower += Physics.gravity.y * GravityRatio * Time.deltaTime;

            _height += (jumpPower + (Physics.gravity.y * GravityRatio)) * Time.deltaTime;

            yield return 0;
        }

        _height = 0;
    }
    public void Jump()
    {
        PetAnimator.SetTrigger(_jump);
    }

    public void SetDestination(Vector3 pos)
    {
        Agent.SetDestination(pos);
    }
}
