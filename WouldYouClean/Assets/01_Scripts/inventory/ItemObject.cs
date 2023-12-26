using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer spriterenderer;

    [SerializeField] private ObjectType itemData;

    public ItemDataSO Item => itemData;

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
            Inventory.Instance.AddItem(itemData, false); // �߰��ϰ�

            Destroy(gameObject); // �����ϰ�
        }
    }

    public void SetItemData(ItemDataSO data)
    {
        itemData = data;
    }

    public void SetPosition(Vector2 pos)
    {
        transform.position = pos;
    }

}
