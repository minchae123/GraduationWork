using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/StageInfo")]
public class StageSO : ScriptableObject
{
    public GameObject stagePref;

    public int player1MoveCount;
    public int player2MoveCount;

    public Color player1Color;
    public Color player2Color;
    public Color targetColor;

    public bool IsClear= false;
}
