using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMover : MonoBehaviour
{
    private PlayerPetController _playerController;
    public float JumpHeight = 5f;
    public float JumpDuration = 0.5f;

    IEnumerator Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;

        while (true)
        {
            if(agent.isOnOffMeshLink)
            {
                yield return StartCoroutine(JumpAgent(agent, JumpHeight, JumpDuration));

                agent.CompleteOffMeshLink();
            }
            yield return null;
        }
    }

    IEnumerator JumpAgent(NavMeshAgent agent, float jumpHeight, float jumpDuration)
    {
        if(_playerController == null)
        {
            _playerController = GetComponent<PlayerPetController>();
        }

        _playerController.Jump();

        OffMeshLinkData offMesh = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = offMesh.endPos + agent.baseOffset * Vector3.up;

        float time = 0f;

        while(time < 1f)
        {
            float height = jumpHeight * (time - (time * time));
            agent.transform.position = Vector3.Lerp(startPos, endPos, time) + height * Vector3.up;

            time += Time.deltaTime / jumpDuration;

            yield return null;
        }
    }
}
