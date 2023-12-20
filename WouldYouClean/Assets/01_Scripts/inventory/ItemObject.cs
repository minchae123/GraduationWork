using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer spriterenderer;

    [SerializeField] private ItemDataSO itemData;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriterenderer.sprite = itemData.itemIcon;
    }

    public void PickUpItem() // �߰�
    {
        if(Inventory.Instance.CheckInventoryIdx(itemData)) // �κ��丮 �ڸ� �������� ������
        {
            Inventory.Instance.AddItem(itemData); // �߰��ϰ�
            Destroy(gameObject); // �����ϰ�
        }
    }
}
