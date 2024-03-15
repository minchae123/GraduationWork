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

    private float _chargingTime;

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

        Booster();
    }

    public void Booster()
    {
        if (Input.GetKey(KeyCode.LeftShift) && _chargingTime < 1.1f)
            _chargingTime += 1 * Time.deltaTime;
        else if (_chargingTime > 0)
            _chargingTime -= 1 * Time.deltaTime;

        if(_chargingTime > 1)
        {
            _maxSpeed = 15;
            _curSpeed = _maxSpeed;
        }
        else if(_chargingTime < 1 && _maxSpeed > 5)
        {
            if (_spaceShipDir.y < 0)
            {
                _maxSpeed -= _acceleration * Time.deltaTime;
            }

            _maxSpeed -= 5 * Time.deltaTime;
            _curSpeed = _maxSpeed;
        }
    }


    public void Move()
    {
        transform.Rotate(0, 0, -_spaceShipDir.x * Time.deltaTime * _rotSpeed);

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
