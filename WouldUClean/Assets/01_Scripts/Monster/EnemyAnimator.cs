using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
	private Animator animator;

	private readonly int walkHash = Animator.StringToHash("speed");
	private readonly int attackHash = Animator.StringToHash("Attack");
	private readonly int deadHash = Animator.StringToHash("Dead");

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void DeadTrigger(bool value)
	{
		if (value)
		{
			animator.SetTrigger(deadHash);
		}
	}

	public void SetWalkAniamtion(float speed)
	{
		animator.SetFloat(walkHash, speed);
	}

	public void AttackTrigger(bool value)
	{
		if(value)
			animator.SetTrigger(attackHash);
	}
}
