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

    public void PickUpItem() // Ãß°¡
    {
        Inventory.Instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
