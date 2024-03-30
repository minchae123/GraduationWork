using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : PoolableMono
{
    [SerializeField] float speed;
    Rigidbody rigid;
    float gravity = 2f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        //rigid.velocity = Vector3.forward * speed;
        rigid.AddForce((Vector3.up + Vector3.forward) * 600);
    }

    public override void Init()
    {
        //그래비티초기화
    }

    private void Update()
    {
    }
}
