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


	public void SetNewPlayers() // �������� �ٲ� ������ �������� �־��ֱ�
	{
		players = stageTrm.GetComponentsInChildren<Movement>(); // ������������ �÷��̾ ã��
		
		moveUIList.ForEach(m => Destroy(m.gameObject));
		moveUIList.Clear();

		moveUIList = new List<MoveUI>();

		for (int i = 0; i < players.Length; ++i)
        {
			MoveUI move = Instantiate(moveUIPref, moveUITrm);
			move.SetUI(players[i].GetComponent<MeshRenderer>().material.color, 1);

			moveUIList.Add(move);
		}

		selectedPlayer = players[selectedNum]; // ó�� �÷��̾�
	}

	public void ChangeMovePlayer()
	{
		selectedNum = ++selectedNum % players.Length; // ���࿡ �÷��̾ 2���̸� 0 1 0 1 0 1 / 3���̸� 0 1 2 0 1 2
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
