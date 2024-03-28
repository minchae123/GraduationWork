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

    public void PickUpItem() // �߰�
    {
        if (Inventory.Instance.CheckInventoryIdx(type)) // �κ��丮 �ڸ� �������� ������
        {
            Inventory.Instance.AddItem(type, false); // �߰��ϰ�

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
