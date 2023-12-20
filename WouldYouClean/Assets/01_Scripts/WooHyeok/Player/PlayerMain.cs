using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    protected Rigidbody2D _rb;
    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;
    protected static bool _isKeyDown = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = transform.Find("Visual").GetComponent<Animator>();
        _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
    }
}
