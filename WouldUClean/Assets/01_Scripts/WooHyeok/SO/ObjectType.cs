using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ObjectType")]
public class ObjectType : ScriptableObject
{
    #region 프로퍼티
    #endregion

    #region 변수들
    public Mesh _ItemMesh;
    public Material _Material;
    public Sprite _ItemSprite;
    public string _ObjectName;
    [TextArea]
    public string _ObjectExplain;

    public int itemPrice;
    #endregion 
}
