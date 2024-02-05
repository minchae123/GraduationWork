using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellingShopTable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Main")]
    public ObjectType currentItem;
    [SerializeField] private int itemAmount;

    [Header("UI")]
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private TextMeshProUGUI itemAmountText;

    private bool isTable = false;
    public bool IsTable => isTable;

    private void Start()
    {
        ResetItem();
    }

    public void SetItem(ObjectType item)
    {
        if (currentItem != null) // ���� ���̺� ������ ���� ���
        {
            Shop.Instance.AddItem(currentItem, itemAmount);
            ResetItem();
        }

        currentItem = item;
        itemImage.sprite = item._ItemIcon; // ������ ���������� �ش� �̹��� �ְ�..
        itemName.text = item._ObjectName; // ������ �̸��� ����
        itemPrice.text = $"{item.itemPrice}���� �Ǹ�"; // ������ ����
        itemAmountText.text = $"{itemAmount}";
    }

    public void ResetItem()
    {
        currentItem = null;
        
        itemImage.sprite = null;
        
        itemName.text = "�������� �巡�� ���ּ���";
        itemPrice.text = string.Empty;
        itemAmountText.text = $"{1}";
        
        itemAmount = 1;
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
        Coin.Instance.AddCoin(currentItem.itemPrice * itemAmount);
        ResetItem();
    }

    public void IncreaseAmount()
    {
        if (Shop.Instance.shopDictionary.TryGetValue(currentItem, out InventoryItem i)) // shopdictionary�� �ִ���
        {
            Shop.Instance.RemoveItem(currentItem);
            itemAmount++;
        }
        else
        {
            print("����");
        }

        UpdateText();
    }
    public void DecreaseAmount()
    {
        if (itemAmount <= 0) return;
        Shop.Instance.AddItem(currentItem);
        itemAmount--;

        UpdateText();
    }

    public void UpdateText()
    {
        itemPrice.text = $"{currentItem.itemPrice*itemAmount}���� �Ǹ�";
        itemAmountText.text = $"{itemAmount}";
    }
}
