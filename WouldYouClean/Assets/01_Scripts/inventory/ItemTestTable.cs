using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemTestTable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;

    public void SetItem(ItemDataSO item)
    {
        itemImage.sprite = item.itemIcon; // ������ ���������� �ش� �̹��� �ְ�..
        itemName.text = item.itemName; // ������ �̸��� ����
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
