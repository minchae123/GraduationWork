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
        Invoke("DestoryObject", 5f); // �������ϱ� �̷��� �س��� ���߿� �ٲٵ簡 ���簡 �ϰ���
    }

    public override void Init()
    {
        //�׷���Ƽ�ʱ�ȭ
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
            print("������");
        }
    }
}
