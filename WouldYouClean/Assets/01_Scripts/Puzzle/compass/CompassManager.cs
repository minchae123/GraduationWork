using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CompassNum
{
    one, two, three, four, five, six, seven, eight, nine
}

public class CompassManager : MonoSingleton<CompassManager>
{
    [SerializeField] private CompassPieceSO piecesData;

    [Header("UI")]
    [SerializeField] public Transform canvas;
    [SerializeField] public Transform unuseParentTrm;
    [SerializeField] public Transform puzzleSlotParentTrm;

    public CompassPieceUI[] compassPiece;
    public PuzzleSlotUI[] puzzleSlot;
    
    private void Start()
    {
        compassPiece = unuseParentTrm.GetComponentsInChildren<CompassPieceUI>();
        puzzleSlot = puzzleSlotParentTrm.GetComponentsInChildren<PuzzleSlotUI>();

        List<int> randList = new List<int>();
        for (int i = 0; i < compassPiece.Length; ++i) randList.Add(i);
        for (int i = 0; i < compassPiece.Length; ++i)
        {
            int randIndex = Random.Range(0, randList.Count);
            compassPiece[i].SetData(piecesData.pieces[randList[randIndex]]);

            randList.RemoveAt(randIndex);
        }

        for (int i = 0; i < puzzleSlot.Length; ++i)
        {
            puzzleSlot[i].num = (CompassNum)i;
        }
    }

    public Transform CheckSlot()
    {
        foreach(PuzzleSlotUI p in puzzleSlot)
        {
            if (p.IsEnter)
            {
                return p.transform; // ���� �����Ͱ� �����ִ� �� ã��
            }
        }

        return null;
    }

    public void HowManyPiecesLeft()
    {
        compassPiece = unuseParentTrm.GetComponentsInChildren<CompassPieceUI>();
        if(compassPiece.Length == 0) // �� ���Կ� �־���!!
        {
            if(CheckAnswer())
            {
                print("����");
            }
            else
            {
                print("��õ�");
            }
        }
    }
    private bool CheckAnswer()
    {
        foreach(PuzzleSlotUI p in puzzleSlot)
        {
            if(!p.EqualOrNot()) // Ʋ�� �� �ϳ��� �ִٸ�
            {
                return false;
            }
        }
        return true;
    }
}
