using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour
{
	[SerializeField] private Movement player1;
	[SerializeField] private Movement player2;

	public void ChageMovePlayer()
	{
		player1.isTurn = !player1.isTurn;
		player2.isTurn = !player2.isTurn;
	}
}
