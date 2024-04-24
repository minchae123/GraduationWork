using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/StageInfo")]
public class StageSO : ScriptableObject
{
    public GameObject stagePref;

    public int LmoveCnt;
    public int RmoveCnt;

    public bool IsClear= false;
}
