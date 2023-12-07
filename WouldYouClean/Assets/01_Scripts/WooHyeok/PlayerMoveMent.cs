using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private float _currentSpeed;

    private Vector2 _direction;
    private Vector2 _moveDirection;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _input.OnMovement += OnMovement;
    }

    private void OnMovement(Vector2 value)
    {
        _direction = value;
    }

    private void CalculateMovement()
    {
        _moveDirection = new Vector2(_direction.x * _currentSpeed, _direction.y * _currentSpeed);
    }

    void Update()
    {
        CalculateMovement();
        _rb.velocity = _moveDirection;
    }
}
