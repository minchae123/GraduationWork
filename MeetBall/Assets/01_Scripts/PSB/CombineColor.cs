using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombineColor : MonoBehaviour
{
    private Material _mat;

    private void Awake()
    {
        _mat = GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _mat.color += other.gameObject.GetComponent<Renderer>().material.color;
            Destroy(other.gameObject);
        }
    }
}
