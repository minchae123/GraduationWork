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
        //Vector3 mousePos = Input.mousePosition;
        //mousePos.z = 0;
        //Vector2 mouseInWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        //Vector3 direction = (Vector3)mouseInWorldPos - transform.position;
        //Vector3 result = Vector3.Cross(Vector2.up, direction); //Vector3.Cross = 외적, 외적은 좌우 판별할 때 유용

        //return result.z > 0;

        if(pMove._direction.x == 0)
            return _spriteRenderer.flipX;

        return pMove._direction.x >= 0;
    }
}
