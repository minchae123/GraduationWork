using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoSingleton<PlayerManager>
{
	[SerializeField] private Transform stageTrm;

	[Header("Player")]
	[SerializeField] private Movement[] players;
	[SerializeField] private Movement selectedPlayer;
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
			move.SetUI(players[i].GetComponent<MeshRenderer>().material.color, players[i].moveCount);

			moveUIList.Add(move);
		}

		selectedPlayer = players[selectedNum]; // 처음 플레이어
		moveUIList[selectedNum].transform.DOScale(1.1f, 0.8f);
	}

	public void ChangeMovePlayer()
	{
		DOTween.Clear();
		moveUIList.ForEach(m => m.transform.DOScale(1f, 0.4f));

		selectedNum = ++selectedNum % players.Length; // 만약에 플레이어가 2명이면 0 1 0 1 0 1 / 3명이면 0 1 2 0 1 2
		selectedPlayer = players[selectedNum];

		moveUIList[selectedNum].transform.DOScale(1.1f, 0.8f);
		selectedPlayer.RayCheck();
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
