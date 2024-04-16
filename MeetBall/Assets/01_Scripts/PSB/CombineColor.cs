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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _mat.color += collision.gameObject.GetComponent<Renderer>().material.color;
            Destroy(collision.gameObject);
        }
    }
}
