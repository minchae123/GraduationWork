using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlanetInSpace : SpaceObject
{
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.isKinematic = true;
    }
}
