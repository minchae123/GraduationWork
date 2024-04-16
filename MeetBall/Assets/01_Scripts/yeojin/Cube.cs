using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool visit;
    public bool Visit => visit;
    private MeshRenderer mr;

    private bool start;
    private bool wall;
    private bool end;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        ResetVisit();
    }

    public void SetStart() // ��ŸƮ ����
    {
        start = true;
        mr.material.color = Color.green;
    }
    public void SetWall() // �� ���� ����
    {
        wall = true;
        mr.material.color = Color.black;
    }
    public void SetEnd() // ������ ����
    {
        end = true;
        mr.material.color = Color.blue;
    }

    public void ResetVisit()
    {
        if(!visit || start || wall)
        {
            return;
        }

        visit = false;
        mr.material.color = Color.gray;
    }

    public void SetVisit()
    {
        if(visit || start || wall)
        {
            return;
        }
        if(end)
        {
            print("������ ����!");
            // ���� ���� �߰�
            return;
        }

        visit = true;
        mr.material.color = Color.red;
    }


    public bool CheckCanGO()
    {
        return visit || wall;
    }
}
