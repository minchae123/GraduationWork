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

    public ColorEnum[] playersColor;    
    public ColorEnum targetColor;

    public bool IsClear= false;
}
