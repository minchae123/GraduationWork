using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopTable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ObjectType currentItem;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;

    private bool isTable = false;
    public bool IsTable => isTable;

    public void SetItem(ObjectType item)
    {
        if (currentItem != null) // ���� ���̺� ������ ���� ���
        {

        }

        currentItem = item;
        itemImage.sprite = item.itemIcon; // ������ ���������� �ش� �̹��� �ְ�..
        itemName.text = item._ObjectName; // ������ �̸��� ����
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isTable = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isTable = false;
    }
}
