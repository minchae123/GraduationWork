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

    private UpgradeStat spaceship;
    private Camera _cam;
    private SpaceshipLaunch _readyToLaunch;

    private SpriteRenderer _sr;

    public bool _isDetected;
    private int _planetIndex;

    public static Action Reset;

    private void Awake()
    {
        spaceship = GameObject.Find("SpaceShip").GetComponent<UpgradeStat>();
        _readyToLaunch = GameObject.Find("SpaceShip").GetComponent<SpaceshipLaunch>();
        _cam = Camera.main;
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
        if (_isDetected && !_readyToLaunch._onSpaceship)
        {
            //TWINKLE
            Debug.Log("TWINKLE");

            if (Input.GetKeyDown(KeyCode.F))
            {
                //GO PLANET
                StartCoroutine(Arrive());
                Debug.Log("LET'S GO");
                _cam.orthographicSize -= .5f * Time.deltaTime;
            }
        }
    }

    private IEnumerator Arrive()
    {
        //Âø·ú
        yield return new WaitForSeconds(3);
        spaceship._curSpeed = 0;
        _readyToLaunch._onSpaceship = true;
    }
}
