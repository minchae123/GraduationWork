using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool visit;
    public bool Visit => visit;
    private MeshRenderer mr;

    private bool start;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        ResetVisit();
    }

    public void SetStart()
    {
        start = true;
        mr.material.color = Color.green;
    }

    public void ResetVisit()
    {
        if(!visit || start)
        {
            return;
        }

        visit = false;
        mr.material.color = Color.gray;
    }

    public void SetVisit()
    {
        if(visit || start)
        {
            return;
        }

        visit = true;
        mr.material.color = Color.red;
    }
}
