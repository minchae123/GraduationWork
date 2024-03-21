using UnityEngine;

[CreateAssetMenu(menuName = "SO/Puzzle/Hidden")]
public class HiddenSO : ScriptableObject
{
    public Sprite _circle;
    public Sprite[] _image;
    public RectTransform[] _left;
    public RectTransform[] _right;
}
