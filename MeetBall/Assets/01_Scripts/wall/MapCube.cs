using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCube : MonoBehaviour
{
    [SerializeField] private Material originMat;
    [SerializeField] private Material visitMat;

    private MeshRenderer mr;

    public bool isVisit = false;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        CancelVisit();
    }

    public void SetVisit()
    {
        isVisit = true;
        mr.material = visitMat;
    }
    public void CancelVisit()
    {
        isVisit = false;
        mr.material = originMat;
    }
}
