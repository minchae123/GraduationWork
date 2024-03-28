using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideObj : MonoBehaviour
{
    public ObjectType type;

    private Mesh mesh;

    private void Awake()
    {
        mesh = GetComponent<Mesh>();
    }
    private void Start()
    {
        mesh = type._ItemMesh;
    }

    public void PickUpItem() // 추가
    {
        if (Inventory.Instance.CheckInventoryIdx(type)) // 인벤토리 자리 남아있을 때에만
        {
            Inventory.Instance.AddItem(type, false); // 추가하고

            Destroy(gameObject); // 삭제하고 여기서 삭제 안하는데 어디서 삭제하는지 모르겟음
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
