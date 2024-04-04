using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlanetInSpace : SpaceObject
{
    public bool found;
    public bool clean;
    public bool interacted;
    [Space(15)]
    public bool rainPlanet;
    public bool snowPlanet;
    public bool windPlanet;
    [SerializeField] private GameObject _detectObj;
    public GameObject inPlanet;

    public Transform LandPos;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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
