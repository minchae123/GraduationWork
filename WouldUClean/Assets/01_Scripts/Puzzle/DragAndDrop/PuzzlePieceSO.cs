using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CompassPieceClass
{
    public CompassNum num;
    public Sprite sprite;
}
[CreateAssetMenu(menuName ="SO/Puzzle/Piece")]
public class PuzzlePieceSO : ScriptableObject
{
    public List<CompassPieceClass> pieces;

    [RuntimeInitializeOnLoadMethod]
    public static void Initialize()//모든 PuzzlePieceSO 객체들을 찾아서 SetNum 메서드 호출
    {
        PuzzlePieceSO[] piecesSO = Resources.FindObjectsOfTypeAll<PuzzlePieceSO>();
        foreach (PuzzlePieceSO p in piecesSO)
        {
            p.SetNum();
        }
    }
    public void SetNum()
    {
        int i = 0;
        foreach(var list in pieces)
        {
            list.num = (CompassNum)i++;
        }
    }
}
