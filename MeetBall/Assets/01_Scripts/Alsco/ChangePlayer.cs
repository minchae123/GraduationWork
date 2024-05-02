using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayer : MonoBehaviour
{
	[Header("Player")]
	[SerializeField] private Movement player1;
	[SerializeField] private Movement player2;

	[Header("UI")]
	[SerializeField] private Image colorImage;
	[SerializeField] private TextMeshProUGUI moveCnt;

	public void FindPlayer()
	{
		
	}

	public void ChageMovePlayer()
	{
		player1.isTurn = !player1.isTurn;
		player2.isTurn = !player2.isTurn;

		if (player1.isTurn) // 1Â÷·ÊÀÏ¶§
		{
			colorImage.color = player1.GetComponent<MeshRenderer>().material.color;
			//moveCnt.text = player1.maxCount - player1.curCount;
		}
		else // 2Â÷·ÊÀÏ¶§
		{
			colorImage.color = player2.GetComponent<MeshRenderer>().material.color;
			//moveCnt.text = player2.maxCount - player2.curCount;
		}
	}
}
