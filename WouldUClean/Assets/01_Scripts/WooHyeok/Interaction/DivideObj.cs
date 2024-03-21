using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideObj : MonoBehaviour
{
    public ObjectType type;

    private SpriteRenderer spriterenderer;


    private void Awake()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        spriterenderer.sprite = type._ItemIcon;
    }

    public void PickUpItem() // �߰�
    {
        if (Inventory.Instance.CheckInventoryIdx(type)) // �κ��丮 �ڸ� �������� ������
        {
            Inventory.Instance.AddItem(type, false); // �߰��ϰ�
            MapManager.Instance.RemoveTrash(this);

            Destroy(gameObject); // �����ϰ� ���⼭ ���� ���ϴµ� ��� �����ϴ��� �𸣰���
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