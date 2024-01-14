using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlanetInSpace : SpaceObject
{
    [SerializeField] private float _radius;
    [SerializeField] private List<PlanetType> _planetType;
    [HideInInspector] public PlanetType _curType;

    private SpriteRenderer _sr;
    private int _planetIndex;

    public bool clean;
    public bool interacted;
    [SerializeField] private GameObject _detectObj;

    public static Action Reset;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = true;

        Reset += ResetPlanet;
        PlanetInSpace.Reset();
    }

    public void ResetPlanet()
    {
        transform.localScale = new Vector2(_radius, _radius);
        _planetIndex = UnityEngine.Random.Range(0, _planetType.Count);
        _curType = _planetType[_planetIndex];
        _sr.sprite = _planetType[_planetIndex]._sr;
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
