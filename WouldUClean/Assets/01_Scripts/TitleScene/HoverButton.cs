using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour
{
    #region ��ŸƮ�� ��ư
    [SerializeField] private Image hoverImg;

    public void BtnHoverIn()
    {
        hoverImg.transform.DOScaleY(1, 0.3f);
    }
    public void BtnHoverOut()
    {
        hoverImg.transform.DOScaleY(0, 0.3f);
    }
    #endregion
}
