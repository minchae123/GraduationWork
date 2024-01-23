using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeSlotUI : MonoBehaviour, IPointerClickHandler
{

    [Header("")]
    public ShopItemSO sellingItem; // ���� �Ȱ� �ִ� ��:��

    [Header("UI")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;

    private void Start()
    {
        SetItem();
    }

    private void SetItem()
    {
        itemIcon.sprite = sellingItem.itemIcon;
        itemName.text = sellingItem.itemName;
    }

    public void OnPointerClick(PointerEventData eventData) // Ŭ��������
    {
        UpgradeManager.Instance.SetItem(sellingItem);
    }
}
