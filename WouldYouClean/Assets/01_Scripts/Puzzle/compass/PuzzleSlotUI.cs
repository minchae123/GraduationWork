using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzleSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public CompassNum num;

    [Header("==============")]
    private RectTransform rect;
    private Image colorImage;

    public CompassPieceUI childPiece;
    public bool IsEnter;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        colorImage = GetComponent<Image>();
    }

    public bool EqualOrNot() => num == childPiece.num ? true : false;

    #region EventData
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent<CompassPieceUI>(out CompassPieceUI p))
        {
            if (childPiece != null)
            {
                if (p.parentSlot != null && p.parentSlot != this) // 새로 들어온 조각이 슬롯에 있었다면
                {
                    PuzzleSlotUI slot = p.parentSlot;

                    childPiece.SetParent(slot);
                    childPiece.transform.SetParent(slot.transform);
                    
                    slot.SetChild(childPiece, slot.transform);
                    
                    childPiece.transform.position = slot.transform.position;
                }
                else // 그ㅜ냥 인벤에 있었다면
                {
                    childPiece.transform.SetParent(CompassManager.Instance.unuseParentTrm);
                }
            }
            else // childPiece가 null이었을 때 (아무것도 없던 슬롯에 드래그드랍했을때
            {
                if(p.parentSlot != null) // 만약 다른 슬롯에서부터 옮겨온 거라면
                {
                    p.parentSlot.childPiece = null; // 해당 슬롯의 childPiece는 삭제
                }
            }
            SetChild(p, eventData.pointerDrag.transform); // 새로 들어온 걸로 세팅
            p.SetParent(this);
        }

        CompassManager.Instance.HowManyPiecesLeft(); // 체크
        colorImage.color = Color.white; // 초기화 안 됐을 경우를 위한 초기화
    }

    public void SetChild(CompassPieceUI p, Transform t)
    {
        childPiece = p;
        t.SetParent(transform);
        t.GetComponent<RectTransform>().position = rect.position;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {

        if (eventData.pointerDrag != null)
        {
            colorImage.color = Color.yellow;
            IsEnter = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            colorImage.color = Color.white;
            IsEnter = false;
        }
    }
    #endregion
}
