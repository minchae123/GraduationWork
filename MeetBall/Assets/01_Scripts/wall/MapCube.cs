using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCube : MonoBehaviour
{
    [SerializeField] private Material originMat;
    [SerializeField] private Material visitMat;

    private MeshRenderer mr;

    private bool visit;
    public bool Visit => visit;


    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        CancelVisit();
    }

    public void SetVisit()
    {
        visit = true;
        mr.material = visitMat;
    }
    public void CancelVisit()
    {
        visit = false;
        mr.material = originMat;
        print("cancel");
    }
}
