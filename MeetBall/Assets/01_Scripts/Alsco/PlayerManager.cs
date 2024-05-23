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

    public void SetNewPlayers(StageSO curStage) // 스테이지 바뀔 때마다 마지막에 넣어주기
    {
        selectedNum = 0;

        players.Clear();
        players = new List<Movement>();

        moveUIList.ForEach(m => Destroy(m.gameObject));

        moveUIList.Clear();
        moveUIList = new List<MoveUI>();

        players = stageTrm.GetComponentsInChildren<Movement>().ToList(); // 스테이지에서 플레이어를 찾아

        for (int i = 0; i < players.Count; ++i)
        {
            MoveUI move = Instantiate(moveUIPref, moveUITrm);

            move.SetUI(curStage.playersColor[i], curStage.playersCount[i]);
            move.UnSelect();

            moveUIList.Add(move);

            players[i].SetPlayer(curStage.playersColor[i], curStage.playersCount[i]);
        }

        targetImage.color = GameManager.Instance.FindColor(curStage.targetColor);

        selectedPlayer = players[selectedNum]; // 처음 플레이어
        moveUIList[selectedNum].Select();

        //players.ForEach(p => print($"foreach1 : {p.TotalCount}"));
    }

    public void ChangeMovePlayer()
    {
        DOTween.Clear();
        moveUIList.ForEach(m => m.UnSelect());

        selectedNum = ++selectedNum % players.Count; // 만약에 플레이어가 2명이면 0 1 0 1 0 1 / 3명이면 0 1 2 0 1 2
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

        if (players.Count <= 1) // 플레이어가 한 명 남았을 경우
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
