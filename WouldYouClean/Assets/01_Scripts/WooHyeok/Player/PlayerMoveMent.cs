using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : PlayerMain
{
    [SerializeField] private InputReader _input;
    [SerializeField] private float _curSpeed;

    private Vector2 _direction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _input.OnMovement += OnMovement;
        _input.OnFKeyDown += OnKeyDown;
    }

    private void OnKeyDown()
    {
        Debug.Log("µÇ´ÂÁö");
        _isKeyDown = true;
    }

    private void OnMovement(Vector2 value)
    {
        _direction = value;
    }

    void Update()
    {
        _rb.velocity = _direction * _curSpeed;
    }
}
