using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetItemTrigger : MonoBehaviour
{
    private ItemObject item;
    private void Awake()
    {
        item = transform.parent.GetComponent<ItemObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<TestPlayer>(out TestPlayer p))
        {
            item.PickUpItem();
        }
    }
}
