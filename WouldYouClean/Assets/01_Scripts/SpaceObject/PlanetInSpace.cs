using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlanetInSpace : SpaceObject
{
    [SerializeField] private float _radius;
    private CircleCollider2D _col;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = true;

        _col = GetComponent<CircleCollider2D>();

        transform.localScale = new Vector2(_radius * 2, _radius * 2); 
    }
}
