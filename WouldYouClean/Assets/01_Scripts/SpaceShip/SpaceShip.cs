using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spaceship : UpgradeStat
{
    [Header("InputSystem")]
    [SerializeField] private InputReader _input;

    private Vector2 _randomPos;

    private Vector2 _spaceShipDir;

    public SpaceObject[] spaceObjects;

    private void Awake()
    {
        _input.OnMovement += OnMove;
    }

    private void OnMove(Vector2 value)
    {
        _spaceShipDir = value;
    }

    private void Start()
    {
        spaceObjects = GameObject.FindObjectsOfType<SpaceObject>();
    }

    private void Update()
    {
        foreach (SpaceObject spaceObject in spaceObjects)
        {
            spaceObject.SetDir(-transform.up * Acceleration());


            if (Vector3.Distance(transform.position, spaceObject.transform.position) >= 60)
            {
                if (Vector3.Distance(transform.position, spaceObject.transform.position) >= 50)
                {
                    _randomPos.x = UnityEngine.Random.Range(-60, 60);
                    _randomPos.y = UnityEngine.Random.Range(-30, 30);
                    spaceObject.transform.position = _randomPos;
                }
            }
        }

        transform.Rotate(0, 0, -_spaceShipDir.x * Time.deltaTime * (_rotSpeed * _curSpeed));
    }

    private float Acceleration()
    {
        LimitSpeed();

        if (_spaceShipDir.y > 0)
        {
            _curSpeed += _acceleration * Time.deltaTime;
        }
        if (_spaceShipDir.y < 0)
        {
            _curSpeed -= _acceleration * Time.deltaTime;
        }
        return Mathf.Clamp(_curSpeed, 0, _maxSpeed);
    }

    private void LimitSpeed()
    {
        if (_curSpeed > _maxSpeed) _curSpeed = _maxSpeed;
        if (_curSpeed < 0) _curSpeed = 0;
    }


}
