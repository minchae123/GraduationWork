using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(menuName = "SO/GimmickExplain")]
public class Gimmick : ScriptableObject
{
    public string[] Explain;
    public VideoClip[] video;
}
