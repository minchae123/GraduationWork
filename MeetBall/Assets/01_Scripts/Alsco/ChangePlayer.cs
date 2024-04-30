using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour
{
	public GameObject player1;
	public GameObject player2;

	public GameObject curPlayer;


	public void ChageMovePlayer()
	{
		curPlayer = curPlayer == player1 ? player2 : player1;
	}
}
