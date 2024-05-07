using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoSingleton<PlayerManager>
{
	[SerializeField] private Transform stageTrm;

	[Header("Player")]
	[SerializeField] private Movement[] players;

	public Movement selectedPlayer;
	private int selectedNum = 0;

	[Header("UI")]
	[SerializeField] private Transform moveUITrm;

	[SerializeField] private MoveUI moveUIPref;
	[SerializeField] private List<MoveUI> moveUIList;


	public void SetNewPlayers() // 스테이지 바뀔 때마다 마지막에 넣어주기
	{
		players = stageTrm.GetComponentsInChildren<Movement>(); // 스테이지에서 플레이어를 찾아
		
		moveUIList.ForEach(m => Destroy(m.gameObject));
		moveUIList.Clear();

		moveUIList = new List<MoveUI>();

		for (int i = 0; i < players.Length; ++i)
        {
			MoveUI move = Instantiate(moveUIPref, moveUITrm);
			move.SetUI(players[i].GetComponent<MeshRenderer>().material.color, 1);

			moveUIList.Add(move);
		}

		selectedPlayer = players[selectedNum]; // 처음 플레이어
	}

	public void ChangeMovePlayer()
	{
		selectedNum = ++selectedNum % players.Length; // 만약에 플레이어가 2명이면 0 1 0 1 0 1 / 3명이면 0 1 2 0 1 2
		selectedPlayer = players[selectedNum];
	}

	public void MoveLeft()
    {
		selectedPlayer.MoveLeft();
	}
	public void MoveRight()
    {
		selectedPlayer.MoveRight();
    }
	public void MoveUp()
    {
		selectedPlayer.MoveUp();
    }
	public void MoveDown()
    {
		selectedPlayer.MoveDown();
    }
}
