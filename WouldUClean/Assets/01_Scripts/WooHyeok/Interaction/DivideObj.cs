using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideObj : MonoBehaviour
{
    public ObjectType type;

    private MeshFilter meshFilter;
    private MeshRenderer meshRen;

    private void Awake()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();
        meshRen = GetComponentInChildren<MeshRenderer>();
    }
    private void Start()
    {
        meshFilter.mesh = type._ItemMesh;
        meshRen.material = type._Material;
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
