using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeSlotUI : MonoBehaviour, IPointerClickHandler
{

    [Header("")]
    public ShopItemSO sellingItem; // 현재 팔고 있는 거:ㄴ

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

    public void OnPointerClick(PointerEventData eventData) // 클릭햇을때
    {
        UpgradeManager.Instance.SetItem(sellingItem);
    }
}
