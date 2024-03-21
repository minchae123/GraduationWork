using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MergeTable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public ObjectType itemData;

    private Image colorImage;
    [SerializeField] private Image childImage;

    private void Start()
    {
        colorImage = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        colorImage.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        colorImage.color = Color.white;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent<MergeItemSlotUI>(out MergeItemSlotUI m))
        {
            if (itemData != null) //�̹� �������� �ش� ���̺��� ���� ���
            {
                //print("�̹� ����");
                MergeTrash.Instance.AddItem(itemData); // �ְ�
            }

            itemData = m.item.itemData;
            childImage.sprite = m.itemImage.sprite;

            MergeTrash.Instance.UseItem(itemData);
        }
    }

    public void ResetTable()
    {
        itemData = null;
        childImage.sprite = null;
    }
}