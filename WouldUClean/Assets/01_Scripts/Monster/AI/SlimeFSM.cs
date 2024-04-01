using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFSM : EnemyFSM
{
	[SerializeField] private float chaseDecision = 6;
	[SerializeField] private float attackDecision = 2;
	[SerializeField] private EnemyAnimator animator;

	public override void Awake()
	{
		base.Awake();
		animator = GetComponentInChildren<EnemyAnimator>();
		
	}

	protected override void Update()
	{
		base.Update();
		if(decision < attackDecision && curState != EnemyState.Attack)
		{
			print(curState);
			ChangeState(EnemyState.Attack);
		}
		else if(decision < chaseDecision && curState != EnemyState.Chase)
		{
			print(curState);
			ChangeState(EnemyState.Chase);
		}
		else if(decision > chaseDecision && curState != EnemyState.Idle)
		{
			print(curState);
			ChangeState(EnemyState.Idle);
		}
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
				{

				}
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
				{

				}
				break;
			case EnemyState.Chase:
				{
				}
				break;
			case EnemyState.Attack:
				{

				}
				break;
			case EnemyState.Die:
				break;
		}
	}

	IEnumerator AttackCoroutine()
	{
		print("¶§¸®±â");
		animator.AttackTrigger(true);
		animator.AttackTrigger(false);
		yield return new WaitForSeconds(1);
	}

	private void OnTriggerEnter(Collider other)
	{
		
	}
}
