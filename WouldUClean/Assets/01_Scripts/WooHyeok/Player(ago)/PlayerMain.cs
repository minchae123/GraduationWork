using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class PlayerMain : MonoBehaviour
{
    protected Rigidbody _rb;
    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;

    public static bool _isFKeyDown = false;
    public static bool _isQKeyDown = false;
    public static bool _isPlain = false;

    private void Start()
    {
        _rb = transform.GetComponent<Rigidbody>();
        _animator = transform.GetChild(0).GetComponent<Animator>();
        _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
    }
}
