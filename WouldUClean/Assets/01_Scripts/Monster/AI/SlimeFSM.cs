using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFSM : EnemyFSM
{
	public override void OnStateEnter(EnemyState state)
	{
		switch (state)
		{
			case EnemyState.Idle:
				break;
			case EnemyState.Chase:
				{
					navMovement.SetSpeed(moveSpeed);
				}
				break;
			case EnemyState.Attack:
				{
					StartCoroutine(AttackCoroutine());
				}
				break;
			case EnemyState.Die:
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
					navMovement.SetSpeed(0);
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

	IEnumerator AttackCoroutine()
	{
		print("¶§¸®±â");
		yield return new WaitForSeconds(1);
	}
}
