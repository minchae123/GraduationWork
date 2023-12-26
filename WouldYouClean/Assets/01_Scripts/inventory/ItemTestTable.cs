using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemTestTable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ObjectType currentItem;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;

    public void SetItem(ObjectType item)
    {
        if(currentItem != null) // ���� ���̺� ������ ���� ���
        {
            Inventory.Instance.AddItem(currentItem, true); // ���� ���� �ٽ� �κ��丮�� �־��ֱ�
        }

        currentItem = item;
        itemImage.sprite = item.itemIcon; // ������ ���������� �ش� �̹��� �ְ�..
        itemName.text = item._ObjectName; // ������ �̸��� ����
    }

    public void RemoveItem() // �ʱ�ȭ
    {
        itemImage.sprite = null;
        itemName.text = string.Empty;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemManager.Instance.CheckInTable(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemManager.Instance.CheckInTable(false);
    }
}
