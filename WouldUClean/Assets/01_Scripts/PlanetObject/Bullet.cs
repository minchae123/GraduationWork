using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField] float speed;
    Vector3 playerDirection;
    Rigidbody rigid;

    private void Start()
    {

        rigid = GetComponent<Rigidbody>();
        playerDirection = (GameManager.Instance._playerTrm.position - transform.position).normalized;
        rigid.velocity = playerDirection * speed;
    }

    public override void Init()
    {
        playerDirection = (GameManager.Instance._playerTrm.position - transform.position).normalized;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("�������� �߰��ؾ��Ѵ�");
            PoolManager.Instance.Push(this);
        }
    }

}
