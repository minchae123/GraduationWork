using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private float _curSpeed;

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
            spaceObject.SetDir(new Vector2(0, -_spaceShipDir.y));
        }

        transform.Rotate(0, 0, -_spaceShipDir.x * Time.deltaTime * _curSpeed);
    }
}
