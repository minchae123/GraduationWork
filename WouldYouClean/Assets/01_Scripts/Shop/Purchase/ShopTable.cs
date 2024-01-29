using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopTable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Main")]
    public ObjectType currentItem;

    [Header("UI")]
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemPrice;

    private bool isTable = false;
    public bool IsTable => isTable;

    public void SetItem(ObjectType item)
    {
        if (currentItem != null) // ���� ���̺� ������ ���� ���
        {
            Shop.Instance.AddItem(currentItem);
        }

        currentItem = item;
        itemImage.sprite = item._ItemIcon; // ������ ���������� �ش� �̹��� �ְ�..
        itemName.text = item._ObjectName; // ������ �̸��� ����
        itemPrice.text = $"{item.itemPrice}���� �Ǹ�"; // ������ ����
    }

    public void ResetItem()
    {
        currentItem = null;
        itemImage.sprite = null;
        itemName.text = string.Empty;
        itemPrice.text = string.Empty;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isTable = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isTable = false;
    }

    public void OnClickSellItem()
    {
        Coin.Instance.AddCoin(currentItem.itemPrice);
        ResetItem();
    }
}
