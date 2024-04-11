using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool cannotVisitCube = false;
    private bool canVisit = true;
    public bool CanVisit => canVisit;
    private MeshRenderer mr;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        ResetVisit();
    }

    public void SetCannotVisitCube()
    {
        cannotVisitCube = true;
        canVisit = false;
        mr.material.color = Color.green;
    }

    public void ResetVisit()
    {
        if(!canVisit || cannotVisitCube)
        {
            print("이미 누가 방문 중 / 방문 불가 큐브");
            return;
        }

        canVisit = false;
        mr.material.color = Color.gray;
    }

    public void SetVisit()
    {
        if(canVisit || cannotVisitCube)
        {
            print("이미 방문 가능 상태 / 방문 불가");
            return;
        }

        canVisit = true;
        mr.material.color = Color.red;
    }
}
