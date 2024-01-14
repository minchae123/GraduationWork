using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ObjectType")]
public class ObjectType : ScriptableObject
{
    #region 프로퍼티
    #endregion

    #region 변수들
    public Sprite _ItemIcon;
    public string _ObjectName;
    [TextArea]
    public string _ObjectExplain;

    public int itemPrice;
    #endregion 
}
