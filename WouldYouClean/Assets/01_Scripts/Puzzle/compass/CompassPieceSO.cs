using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CompassPieceClass
{
    public CompassNum num;
    public Sprite sprite;
}
[CreateAssetMenu(menuName ="SO/Puzzle/Compass")]
public class CompassPieceSO : ScriptableObject
{
    public List<CompassPieceClass> pieces;

    public void SetNum()
    {
        int i = 0;
        foreach(var list in pieces)
        {
            list.num = (CompassNum)i++;
        }
    }

    private void OnEnable()
    {
        SetNum();
    }
}
