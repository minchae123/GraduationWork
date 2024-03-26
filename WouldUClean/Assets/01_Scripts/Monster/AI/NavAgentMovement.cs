using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentMovement : MonoBehaviour
{
    [SerializeField]
    private float knockbackSpeed = 20f, gravity = -9.8f, knockbackTime = 0.4f;
    private float verticalVelocity;
    private Vector3 knockbackVelocity;
    private Vector3 movementVelocity;

    private NavMeshAgent navAgent;
    public NavMeshAgent NavAgent => navAgent;

    private CharacterController characterController;
    private bool isControllerMode = false;
    private float knockbackStartTime;
    private Action EndKnockBackAction;

    public AIActionData aiActionData;

    protected virtual void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        aiActionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public void SetSpeed(float speed = 4)
    {
        navAgent.speed = speed; 
    }

    public void MoveToTarget(Vector3 pos)
    {
        navAgent.SetDestination(pos);
    }

    public bool CheckIsArrived()
    {
        if (navAgent.pathPending == false && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            return true;
        }

        return false;
    }

    public void StopImmediately()
    {
        navAgent.SetDestination(transform.position); //자기자신을 목적지로 놓으면 바로 정지됨.
    }

    public void StopNavigation()
    {
        navAgent.isStopped = true; //네브 에이전트를 멈춰주고
    }

    public void KnockBack(Action EndAction = null)
    {
        navAgent.enabled = false;
        knockbackStartTime = Time.time; //넉백 시작 시간을 측정
        isControllerMode = true;
        knockbackVelocity = aiActionData.HitNormal * -1 * knockbackSpeed;

        EndKnockBackAction = EndAction;
    }

    private bool CalculateKnockBackMovement()
    {
        float spendTime = Time.time - knockbackStartTime;
        float ratio = spendTime / knockbackTime;
        movementVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, ratio) * Time.fixedDeltaTime;
        return ratio < 1;
    }

    public void ResetNavAgent()
    {
        characterController.enabled = true;
        navAgent.enabled = true;
        navAgent.isStopped = false;
    }

    private void FixedUpdate()
    {
        if (isControllerMode == false) return;

        if (characterController.isGrounded == false)
        {
            verticalVelocity = gravity * Time.fixedDeltaTime;
        }
        else
        {
            verticalVelocity = gravity * 0.3f * Time.fixedDeltaTime;
        }

        if (CalculateKnockBackMovement())
        {
            Vector3 move = movementVelocity + verticalVelocity * Vector3.up;
            characterController.Move(move);
        }
        else
        {
            isControllerMode = false; //콘트롤러 모드 꺼주고
            characterController.enabled = false;
            EndKnockBackAction?.Invoke();
        }
    }
}
