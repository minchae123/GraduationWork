using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlanetInSpace : SpaceObject
{
    [SerializeField] private float _radius;

    public bool found;
    public bool clean;
    public bool interacted;
    [SerializeField] private GameObject _detectObj;
    public GameObject inPlanet;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void ResetPlanet()
    {
        transform.localScale = new Vector2(_radius, _radius);
    }

    private void Update()
    {
        if (clean) _detectObj.SetActive(false);
        else
        {
            if (interacted) _detectObj.SetActive(true);
            else _detectObj.SetActive(false);
        }
    }
}
