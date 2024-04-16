using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Stage")]
public class StageSO : ScriptableObject
{
    public GameObject stage;

    public int LmoveCnt;
    public int RmoveCnt;

    public bool IsClear = false;
}
