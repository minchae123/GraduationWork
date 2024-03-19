using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public CompassNum num;

    [Header("==============")]
    private RectTransform rect;

    public CompassPieceUI childPiece;
    public bool IsEnter;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public bool EqualOrNot() => num == childPiece.num ? true : false;

    #region EventData
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.TryGetComponent<CompassPieceUI>(out CompassPieceUI p))
        {
            if(childPiece!=null)
            {
                childPiece.transform.SetParent(CompassManager.Instance.unuseParentTrm); // 옮겨주고
            }

            childPiece = p;
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsEnter = false;
    }
    #endregion
}
