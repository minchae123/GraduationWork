using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFSM : EnemyFSM
{
	[SerializeField] private float chaseDecision = 6;
	[SerializeField] private float attackDecision = 2;
	[SerializeField] private EnemyAnimator animator;

	private Slime slime;

	private bool isAttack;

	public override void Awake()
	{
		base.Awake();
		animator = GetComponentInChildren<EnemyAnimator>();
		slime = GetComponent<Slime>();
	}

	protected override void Update()
	{
		base.Update();
		if(decision < attackDecision && curState != EnemyState.Attack)
		{
			ChangeState(EnemyState.Attack);
			print(curState);
		}
		else if(decision < chaseDecision && decision > attackDecision && curState != EnemyState.Chase)
		{
			ChangeState(EnemyState.Chase);
			print(curState);
		}
		else if(decision > chaseDecision && curState != EnemyState.Idle)
		{
			ChangeState(EnemyState.Idle);
			print(curState);
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
					print("¤²¤·");
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
		while(true)
		{
			animator.AttackTrigger(true);
			isAttack = true; 
			animator.AttackTrigger(false);
			yield return new WaitForSeconds(1.5f);
			isAttack = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Trash"))
		{
			slime.ReduceHP(5);
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player") && isAttack)
		{
			FindObjectOfType<PlayerHp>().OnDamage(4);
		}
	}

}
