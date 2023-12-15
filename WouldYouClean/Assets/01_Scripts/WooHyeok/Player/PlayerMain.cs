using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    protected Rigidbody2D _rb;
    protected SpriteRenderer _spriteRenderer;
    protected static bool _isKeyDown = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
    }
}
