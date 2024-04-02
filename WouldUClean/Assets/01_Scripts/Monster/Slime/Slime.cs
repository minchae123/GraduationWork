using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
	[SerializeField] private int hp;

	public void ReduceHP(int h)
	{
		hp -= h;

		if (hp < 0)
		{
			Die();
		}
	}

	private void Die()
	{
		GetComponentInChildren<EnemyAnimator>().DeadTrigger(true);
		Destroy(gameObject, 1f);
	}
}
