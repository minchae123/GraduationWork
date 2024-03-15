using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIUFO : SpaceObject
{
    Rigidbody2D rigid;
    public float speed;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 movement = transform.up * Time.deltaTime * speed;
        rigid.velocity = movement;

        Vector2 direction = (Vector2)GameManager.Instance.SpaceShipTrm.position - rigid.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rigid.rotation = angle;
    }
}
