using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

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

	[SerializeField] private Image targetImage;

	public void SetNewPlayers(StageSO curStage) // �������� �ٲ� ������ �������� �־��ֱ�
	{
		players = stageTrm.GetComponentsInChildren<Movement>(); // ������������ �÷��̾ ã��
		
		moveUIList.ForEach(m => Destroy(m.gameObject));
		moveUIList.Clear();

		moveUIList = new List<MoveUI>();

		for (int i = 0; i < players.Length; ++i)
        {
			MoveUI move = Instantiate(moveUIPref, moveUITrm);
			//Color playerColor = (i % 2 == 0) ? curStage.player1Color : curStage.player2Color;
			//move.SetUI(playerColor, players[i].moveCount);

			moveUIList.Add(move);
		}

		targetImage.color = curStage.targetColor;

		selectedPlayer = players[selectedNum]; // ó�� �÷��̾�
		moveUIList[selectedNum].transform.DOScale(1.1f, 0.8f);
	}

	public void ChangeMovePlayer()
	{
		DOTween.Clear();
		moveUIList.ForEach(m => m.transform.DOScale(1f, 0.4f));

		selectedNum = ++selectedNum % players.Length; // ���࿡ �÷��̾ 2���̸� 0 1 0 1 0 1 / 3���̸� 0 1 2 0 1 2
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
