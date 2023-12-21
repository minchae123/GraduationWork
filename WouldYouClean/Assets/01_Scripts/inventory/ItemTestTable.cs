using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTestTable : MonoBehaviour
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
}
