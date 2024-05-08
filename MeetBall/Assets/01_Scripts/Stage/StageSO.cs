using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/StageInfo")]
public class StageSO : ScriptableObject
{
    public GameObject stagePref;

    public Movement[] players;

    public int[] playersCount;

    public Color[] playersColor;    
    public Color targetColor;

    public bool IsClear= false;

    public void SetPlayers()
    {
        for(int i = 0; i < players.Length; ++i)
        {
            players[i].SetPlayer(playersColor[i], playersCount[i]);
        }
    }
}
