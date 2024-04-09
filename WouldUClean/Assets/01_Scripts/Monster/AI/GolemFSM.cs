using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemFSM : EnemyFSM
{
    [SerializeField] GameObject rock;

    private EnemyAnimator animator;
    private EnemyHealth _enemyHealth;


    public override void Awake()
    {
        base.Awake();
        animator = GetComponentInChildren<EnemyAnimator>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    protected override void Update()
    {
        base.Update();
        if (decision < attackDecision && curState != EnemyState.Attack)
        {
            ChangeState(EnemyState.Attack);
            print(curState);
        }
        else if (decision < chaseDecision && decision > attackDecision && curState != EnemyState.Chase)
        {
            ChangeState(EnemyState.Chase);
            print(curState);
        }
        else if (decision > chaseDecision && curState != EnemyState.Idle)
        {
            ChangeState(EnemyState.Idle);
            print(curState);
        }
        //print("거리: " + decision);
        //print(navMovement.NavAgent.isStopped);
    }

    public override void OnStateEnter(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                {
                }
                break;
            case EnemyState.Chase:
                {
                    navMovement.PlayNavigation();
                    animator.SetWalkAniamtion(1);
                }
                break;
            case EnemyState.Attack:
                {
                    StartCoroutine(AttackCoroutine());
                    navMovement.StopNavigation();
                }
                break;
            case EnemyState.Die:
                {
                    animator.DeadTrigger(true);
                }
                break;
        }
    }

    public override void OnStateExit(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Chase:
                {
                    animator.SetWalkAniamtion(-1);
                    navMovement.StopNavigation();
                }
                break;
            case EnemyState.Attack:
                {
                    StopCoroutine(AttackCoroutine());
                }
                break;
            case EnemyState.Die:
                break;
        }
    }

    public override void StateFixedUpdate(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Chase:
                {
                    navMovement.MoveToTarget(targetTrm.position);
                }
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.Die:
                break;
        }
    }

    public override void StateUpdate(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Chase:
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.Die:
                break;
        }
    }

    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            animator.AttackTrigger(true);
            animator.AttackTrigger(false);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetTrm.position - transform.position), 1 * Time.deltaTime);
            print("보기");
            yield return null;
        }
    }

    public void RockThrowEvent()
    {
        Vector3 direction = (targetTrm.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject obj = Instantiate(rock, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), Quaternion.identity);

        Vector3 dir = targetTrm.position - transform.position;
        //obj.GetComponent<Rigidbody>().velocity = dir * obj.GetComponent<Rock>().Speed;
        print("돌 던지기 이벤트");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Trash"))
        {
            _enemyHealth.TakeDamage(5);
        }
    }
}
