using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
	[SerializeField] private int hp;

	public void ReduceHP(int h)
	{
		hp -= h;
	}


}
