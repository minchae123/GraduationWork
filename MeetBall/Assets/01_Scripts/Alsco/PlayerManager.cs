using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [SerializeField] private Transform stageTrm;

    [Header("Player")]
    [SerializeField] private List<Movement> players;
    [SerializeField] private Movement selectedPlayer;

    private int selectedNum = 0;

    [Header("UI")]
    [SerializeField] private Transform moveUITrm;

    [SerializeField] private MoveUI moveUIPref;
    [SerializeField] private List<MoveUI> moveUIList;

    [SerializeField] private Image targetImage;

    public void SetNewPlayers(StageSO curStage) // �������� �ٲ� ������ �������� �־��ֱ�
    {
        selectedNum = 0;

        players.Clear();
        players = new List<Movement>();

        moveUIList.ForEach(m => Destroy(m.gameObject));

        moveUIList.Clear();
        moveUIList = new List<MoveUI>();

        players = stageTrm.GetComponentsInChildren<Movement>().ToList(); // ������������ �÷��̾ ã��

        for (int i = 0; i < players.Count; ++i)
        {
            MoveUI move = Instantiate(moveUIPref, moveUITrm);

            move.SetUI(curStage.playersColor[i], curStage.playersCount[i]);
            move.UnSelect();

            moveUIList.Add(move);

            players[i].SetPlayer(curStage.playersColor[i], curStage.playersCount[i]);
        }

        targetImage.color = GameManager.Instance.FindColor(curStage.targetColor);

        selectedPlayer = players[selectedNum]; // ó�� �÷��̾�
        moveUIList[selectedNum].Select();

        //players.ForEach(p => print($"foreach1 : {p.TotalCount}"));
    }

    public void ChangeMovePlayer()
    {
        DOTween.Clear();
        moveUIList.ForEach(m => m.UnSelect());

        selectedNum = ++selectedNum % players.Count; // ���࿡ �÷��̾ 2���̸� 0 1 0 1 0 1 / 3���̸� 0 1 2 0 1 2
        selectedPlayer = players[selectedNum];

        moveUIList[selectedNum].Select();

        selectedPlayer.RayCheck();
    }

    public void DestroyPlayer(Movement player)
    {
        players.Remove(player);

        selectedNum = 0;
        selectedPlayer = players[selectedNum];

        Destroy(player.gameObject);

        if (players.Count <= 1) // �÷��̾ �� �� ������ ���
        {
        }
    }

    public void MoveLeft()
    {
        selectedPlayer.MoveLeft();
        UpdateUI();
    }
    public void MoveRight()
    {
        selectedPlayer.MoveRight();
        UpdateUI();
    }
    public void MoveUp()
    {
        selectedPlayer.MoveUp();
        UpdateUI();
    }
    public void MoveDown()
    {
        selectedPlayer.MoveDown();
        UpdateUI();
    }

    public void UpdateUI()
    {
        moveUIList[selectedNum].UpdateMove(selectedPlayer.TotalCount - selectedPlayer.curCount);
    }
}
