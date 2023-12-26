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

    public void PickUpItem() // 추가
    {
        if(Inventory.Instance.CheckInventoryIdx(itemData)) // 인벤토리 자리 남아있을 때에만
        {
            Inventory.Instance.AddItem(itemData, false); // 추가하고

            Destroy(gameObject); // 삭제하고
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
