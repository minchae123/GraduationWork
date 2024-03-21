using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : PlayerMain
{
    PlayerMoveMent pMove;

    private void Awake()
    {
        pMove = GetComponent<PlayerMoveMent>();
    }

    private void Update()
    {
        _spriteRenderer.flipX = Direction();
    }

    public bool Direction()
    {
        if(pMove._direction.x == 0)
            return _spriteRenderer.flipX;

        return pMove._direction.x <= 0;
    }
}
