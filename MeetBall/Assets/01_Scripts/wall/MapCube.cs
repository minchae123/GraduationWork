using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCube : MonoBehaviour
{
    [SerializeField] private Material originMat;
    [SerializeField] private Material visitMat;
    [SerializeField] private Material startMat;

    private MeshRenderer mr;

    public bool isVisit = false;
    public bool isStart = false;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        CancelVisit();
    }

    public void SetStart()
    {
        isStart = true;
        mr.material = startMat;
        print("start");
    }
    public void SetVisit()
    {
        isVisit = true;
        mr.material = visitMat;
        print("visit");
    }
    public void CancelVisit()
    {
        isVisit = false;
        mr.material = originMat;
        print("cancel");
    }
}
