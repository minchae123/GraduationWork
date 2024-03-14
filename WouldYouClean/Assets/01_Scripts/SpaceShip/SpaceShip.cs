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

    [SerializeField] private SpaceBackground background;

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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _maxSpeed = 10;
            _curSpeed = 10;
        }
        else
            _maxSpeed = 5;
    }

    public void Move()
    {
        transform.Rotate(0, 0, -_spaceShipDir.x * Time.deltaTime * (_rotSpeed * _curSpeed));

        foreach (SpaceObject spaceObject in spaceObjects)
        {
            spaceObject.SetDir(-transform.up * Acceleration());
        }

        //우주배경
        background.SetDir(Acceleration() * Time.deltaTime * transform.up);
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
        if (_curSpeed < 0f) _curSpeed = 0f;
    }
}
