using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideObj : MonoBehaviour
{
    public ObjectType type;
    public ObjectType Item => type;

    private SpriteRenderer spriterenderer;


    private void Awake()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        spriterenderer.sprite = type.itemIcon;
    }

    public void PickUpItem() // 추가
    {
        if (Inventory.Instance.CheckInventoryIdx(type)) // 인벤토리 자리 남아있을 때에만
        {
            Inventory.Instance.AddItem(type, false); // 추가하고

            Destroy(gameObject); // 삭제하고
        }
    }

    public void SetItemData(ObjectType data)
    {
        type = data;
    }

    public void SetPosition(Vector2 pos)
    {
        transform.position = pos;
    }

}
