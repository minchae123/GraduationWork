using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceShip : UpgradeStat
{
    [Header("InputSystem")]
    [SerializeField] private InputReader _input;

    private Vector2 _spaceShipDir;
    public SpaceObject[] spaceObjects;

    private void OnMove(Vector2 value)
    {
        _spaceShipDir = value;
    }

    private void Start()
    {
        _input.OnMovement += OnMove;
        spaceObjects = GameObject.FindObjectsOfType<SpaceObject>();
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Rotate(0, 0, -_spaceShipDir.x * Time.deltaTime * (_rotSpeed * _curSpeed));

        foreach (SpaceObject spaceObject in spaceObjects)
        {
            spaceObject.SetDir(-transform.up * Acceleration());
        }
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
        return Mathf.Clamp(_curSpeed, 1f, _maxSpeed);
    }

    private void LimitSpeed()
    {
        if (_curSpeed > _maxSpeed) _curSpeed = _maxSpeed;
        if (_curSpeed < 1f) _curSpeed = 1f;
    }
}
