using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Puzzle/Hidden")]
public class HiddenSO : ScriptableObject
{
    public int _picIdx;
    public Sprite _image;
    public RectTransform _parent;
}
