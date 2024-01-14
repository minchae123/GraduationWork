using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ObjectType")]
public class ObjectType : ScriptableObject
{
    #region ������Ƽ
    #endregion

    #region ������
    public Sprite _ItemIcon;
    public string _ObjectName;
    [TextArea]
    public string _ObjectExplain;

    public int itemPrice;
    #endregion 
}
