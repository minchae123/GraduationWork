using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : PoolableMono
{
    Rigidbody rigid;
    float gravity = 2f;

    public float Speed;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        //rigid.velocity = Vector3.forward * speed;
        //rigid.AddForce((Vector3.up + Vector3.forward) * 600);
        //rigid.AddForce((Vector3.forward) * 1000);
        Invoke("DestoryObject", 5f); // 귀찮으니까 이렇게 해놓기 나중에 바꾸든가 말든가 하겠지
    }

    public override void Init()
    {
        //그래비티초기화
    }

    private void Update()
    {
    }

    private void DestoryObject()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("아프기");
        }
    }
}
