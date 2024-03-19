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
                if (p.parentSlot != null && p.parentSlot != this) // ���� ���� ������ ���Կ� �־��ٸ�
                {
                    PuzzleSlotUI slot = p.parentSlot;

                    childPiece.SetParent(slot);
                    childPiece.transform.SetParent(slot.transform);
                    
                    slot.SetChild(childPiece, slot.transform);
                    
                    childPiece.transform.position = slot.transform.position;
                }
                else // �פ̳� �κ��� �־��ٸ�
                {
                    childPiece.transform.SetParent(CompassManager.Instance.unuseParentTrm);
                }
            }
            else // childPiece�� null�̾��� �� (�ƹ��͵� ���� ���Կ� �巡�׵��������
            {
                if(p.parentSlot != null) // ���� �ٸ� ���Կ������� �Űܿ� �Ŷ��
                {
                    p.parentSlot.childPiece = null; // �ش� ������ childPiece�� ����
                }
            }
            SetChild(p, eventData.pointerDrag.transform); // ���� ���� �ɷ� ����
            p.SetParent(this);
        }

        CompassManager.Instance.HowManyPiecesLeft(); // üũ
        colorImage.color = Color.white; // �ʱ�ȭ �� ���� ��츦 ���� �ʱ�ȭ
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
