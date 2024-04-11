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
            print("Dddddd");
            return;
        }

        canVisit = false;
        mr.material.color = Color.gray;
    }

    public void SetVisit()
    {
        if(canVisit || cannotVisitCube)
        {
            return;
        }

        canVisit = true;
        mr.material.color = Color.red;
    }
}
