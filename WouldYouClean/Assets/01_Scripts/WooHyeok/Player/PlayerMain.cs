using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    protected Rigidbody2D _rb;
    protected SpriteRenderer _spriteRenderer;
    protected bool _isKeyDown = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //_spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }
}
