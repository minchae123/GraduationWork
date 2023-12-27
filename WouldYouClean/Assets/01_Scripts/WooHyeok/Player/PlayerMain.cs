using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class PlayerMain : MonoBehaviour
{
    protected Rigidbody2D _rb;
    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;
    public static bool _isKeyDown = false;
    public static bool _isPlain = false;

    private void Start()
    {
        _rb = transform.GetComponent<Rigidbody2D>();
        _animator = transform.Find("Visual").GetComponent<Animator>();
        _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
    }
}
