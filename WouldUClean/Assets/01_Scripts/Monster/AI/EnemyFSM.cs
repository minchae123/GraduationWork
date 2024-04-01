using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle, Chase, Attack, Die
}

public abstract class EnemyFSM : MonoBehaviour
{
    public Transform targetTrm;
    [SerializeField] protected float moveSpeed = 10;

    protected EnemyState curState = EnemyState.Idle;
    protected NavAgentMovement navMovement;

    protected float decision;

    public virtual void Awake()
    {
        navMovement = GetComponent<NavAgentMovement>();
        targetTrm = GameManager.Instance._playerTrm; // GameManager 넣으면 플레이어 위치 넣어주기
    }

    public virtual void Initialize()
    {
        ChangeState(curState);
    }

    protected virtual void Update()
    {
        decision = Vector3.Distance(transform.position, targetTrm.position);
        StateUpdate(curState);
    }

    protected virtual void FixedUpdate()
    {
        StateFixedUpdate(curState);
    }

    public virtual void ChangeState(EnemyState newState)
    {
        if (newState == curState)
            return;
        OnStateExit(curState);
        curState = newState;
        OnStateEnter(curState);
    }

    public abstract void OnStateEnter(EnemyState state);

    public abstract void StateUpdate(EnemyState state);

    public abstract void StateFixedUpdate(EnemyState state);

    public abstract void OnStateExit(EnemyState state);
}
