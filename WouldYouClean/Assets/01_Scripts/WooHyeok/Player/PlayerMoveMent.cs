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
        _input.OnMousePos += OnMousePos;
        _input.OnLeftMouseClick += OnLeftMouseClick;
    }

    private void OnKeyDown(bool value)
    {
        if (value == true)
            _isKeyDown = true;
        else
            _isKeyDown = false;
    }

    private void OnMovement(Vector2 value)
    {
        _direction = value;
        MoveAnim();
    }

    private void MoveAnim()
    {
        _animator.SetFloat("x", Mathf.Abs(_direction.x) / _curSpeed);

        if (Mathf.Abs(_direction.x) / _curSpeed == 0)
            _animator.SetFloat("x", Mathf.Abs(_direction.y) / _curSpeed);
    }

    private void OnMousePos(Vector2 value)
    {

    }

    private void OnLeftMouseClick()
    {
        print("마우스 클릭 되었달ㅇ통래ㅑㄴ");
    }

    void Update()
    {
        _rb.velocity = _direction * _curSpeed;
    }
}
