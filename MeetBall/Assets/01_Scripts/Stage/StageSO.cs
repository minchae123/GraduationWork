using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName ="SO/StageInfo")]
public class StageSO : ScriptableObject
{
    public GameObject stagePref;

    private Movement[] players;

    public int[] playersCount;

    public Color[] playersColor;    
    public Color targetColor;

    public bool IsClear= false;

    public void SetPlayers()
    {
        players = stagePref.transform.GetComponentsInChildren<Movement>();

        if(players.Length != playersCount.Length || players.Length != playersColor.Length)
        {
            Debug.LogError("플레이어랑 안 맞소");
            return;
        }

        for (int i = 0; i < players.Length; ++i)
        {
            players[i].SetPlayer(playersColor[i], playersCount[i]);
        }
    }
}
