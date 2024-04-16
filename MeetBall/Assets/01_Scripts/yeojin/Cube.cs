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

    public void SetStart() // 스타트 지점
    {
        start = true;
        mr.material.color = Color.green;
    }
    public void SetWall() // 못 가는 지점
    {
        wall = true;
        mr.material.color = Color.black;
    }
    public void SetEnd() // 가야할 지점
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
            print("목적지 도달!");
            // 성공 로직 추가
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
