using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CompassPieceUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public CompassNum num;

    [Header("==============")]
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    #region 이벤트
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(CompassManager.Instance.canvas);// 최상단으로 보이게 하려구
        transform.SetAsLastSibling();//이것도 위랑 같은이유

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        if (CompassManager.Instance.FindWhatSlot() == null) // 슬롯 위가 아닐 경우
        {
            transform.SetParent(CompassManager.Instance.unuseParentTrm);
        }

        CompassManager.Instance.HowManyPiecesLeft(); // 체크
    }
#endregion  

    public void SetData(CompassPieceClass data)
    {
        num = data.num;
        transform.Find("image").GetComponent<Image>().sprite = data.sprite;
    }
}
