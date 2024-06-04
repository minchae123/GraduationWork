using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OneStepCube : MapCube
{
    private MeshDestroy _meshDestroy;

    private void Awake()
    {
        _meshDestroy = GetComponent<MeshDestroy>();
    }

    private void OnTriggerExit(Collider other)
    {
        //if()
        if(other.CompareTag("Player"))
        {
            Debug.Log("asaad");
            _meshDestroy.DestroyMesh();
            Destroy(_meshDestroy, 2);
        }
    }
}
