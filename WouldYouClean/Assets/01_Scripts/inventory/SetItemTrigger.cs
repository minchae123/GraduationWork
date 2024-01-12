using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetItemTrigger : MonoBehaviour
{
    private DivideObj item;
    private void Awake()
    {
        item = GetComponentInParent<DivideObj>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMoveMent>(out PlayerMoveMent p))//testPlayer���� playermovement�� �ٲ�
        {
            Debug.Log(item);
            item.PickUpItem();
        }
    }
}
