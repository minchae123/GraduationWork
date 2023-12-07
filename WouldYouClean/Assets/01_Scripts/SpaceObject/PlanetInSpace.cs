using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInSpace : SpaceObject
{
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
}
