using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerPetController : MonoBehaviour
{
    public Animator PetAnimator;
    public NavMeshAgent Agent;

    public float JumpPower = 20f;
    public float GravityRatio = 1f;

    private AgentMover _agentMover;

    private float _height;
    private float _baseOffset;
    private float _jumpCooldown = 0;

    //private bool eat = false;
    //private bool rest = false;

    private int _moveSpeed;
    private int _jump;
    //private int _eat;
    //private int _rest;

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
        //_states.Add(Animator.StringToHash("Base.Eat"), "Eat");
        //_states.Add(Animator.StringToHash("Base.Rest"), "Rest");

        _moveSpeed = Animator.StringToHash("moveSpeed");
        _jump = Animator.StringToHash("jump");
        //_eat = Animator.StringToHash("eat");
        //_rest = Animator.StringToHash("rest");
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
