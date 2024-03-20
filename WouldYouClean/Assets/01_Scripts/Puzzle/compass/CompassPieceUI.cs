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
    public PuzzleSlotUI parentSlot;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    #region 이벤트
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(DragPuzzleManager.Instance.canvas);// 최상단으로 보이게 하려구
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
        if (DragPuzzleManager.Instance.CheckSlot() == null) // 슬롯 위가 아닐 경우
        {
            parentSlot.SetChild(null);
            SetParent(null);
            transform.SetParent(DragPuzzleManager.Instance.unuseParentTrm);
        }
    }
#endregion  
    public void SetParent(PuzzleSlotUI p)
    {
        parentSlot = p;
    }
    public void SetData(CompassPieceClass data)
    {
        num = data.num;
        transform.Find("image").GetComponent<Image>().sprite = data.sprite;
    }
}
