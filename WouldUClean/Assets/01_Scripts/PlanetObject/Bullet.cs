using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField] float speed;
    Vector3 playerDirection;
    Rigidbody rigid;

    public override void Init()
    {
        rigid = GetComponent<Rigidbody>();
        playerDirection = (GameManager.Instance._playerTrm.position - transform.position).normalized;
        rigid.velocity = playerDirection * speed;
    }

    private void OnEnable()
    {
    }

    private void Update()
    {
        //transform.position += Vector3.forward * speed * Time.deltaTime;
    }
}
