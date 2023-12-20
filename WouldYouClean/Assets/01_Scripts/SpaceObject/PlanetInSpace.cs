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

    private CircleCollider2D _col;
    private SpriteRenderer _sr;

    public bool _isDetected;
    private int _planetIndex;

    public static Action Reset;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _col = GetComponent<CircleCollider2D>();
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
        if (_isDetected)
        {
            //TWINKLE
            Debug.Log("TWINKLE");

            if (Input.GetKeyDown(KeyCode.F))
            {
                //GO PLANET
                Debug.Log("LET'S GO");
            }
        }
    }
}
