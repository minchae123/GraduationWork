using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PurchaseSlotUI : MonoBehaviour, IPointerClickHandler
{

    [Header("")]
    public ShopItemSO sellingItem; // ���� �Ȱ� �ִ� ��:��

    [Header("UI")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;

    public void SetItem()
    {
        itemIcon.sprite = sellingItem.itemIcon;
        itemName.text = sellingItem.itemName;
    }

    public void OnPointerClick(PointerEventData eventData) // Ŭ��������
    {
        Shop.Instance.SetItem(sellingItem);
    }
}
