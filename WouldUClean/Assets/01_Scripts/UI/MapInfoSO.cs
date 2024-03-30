using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/UI/Map")]
public class MapInfoSO : ScriptableObject
{
    public Sprite mapSprite;

    [Range(0, 5)] public int mapDifficulty;
    [Range(0, 5)] public int trashValue;
}
