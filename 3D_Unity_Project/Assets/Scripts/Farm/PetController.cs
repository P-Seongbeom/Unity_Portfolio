using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetController : MonoBehaviour
{
    public Animator PetAnimator;
    public NavMeshAgent Agent;

    public float JumpPower;
    public float GravityRatio;

    private float _height;
    private float _baseOffset;
    private float _jumpCooldown = 0;

    public int _moveSpeed;
    private int _jump;
    private int _attack;
    private int _skill;
    private int _die;

    private Dictionary<int, string> _states;

    Vector3 speed;

    void Start()
    { 
        _height = 0;
        _baseOffset = Agent.baseOffset;
        
        _moveSpeed = Animator.StringToHash("moveSpeed");
        _jump = Animator.StringToHash("jump");
        _attack = Animator.StringToHash("attack");
        _skill = Animator.StringToHash("skill");
        _die = Animator.StringToHash("die");

        //_states = new Dictionary<int, string>();
        //_states.Add(Animator.StringToHash("idle"), "Idle");
        //_states.Add(Animator.StringToHash("move"), "Move");
        //_states.Add(Animator.StringToHash("jump"), "Jump");
        //_states.Add(Animator.StringToHash("attack"), "Attack");
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
        JumpMotion();

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
    public void JumpMotion()
    {
        PetAnimator.SetTrigger(_jump);
    }

    public void AttackMotion()
    {
        PetAnimator.SetTrigger(_attack);
    }

    public void SkillMotion()
    {
        PetAnimator.SetTrigger(_skill);
    }

    public void DieMotion()
    {
        PetAnimator.SetTrigger(_die);
    }

    public void SetDestination(Vector3 pos)
    {
        Agent.SetDestination(pos);
    }
}
