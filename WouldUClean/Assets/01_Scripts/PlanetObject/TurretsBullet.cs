using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretsBullet : MonoBehaviour
{
    Vector3 dir;
    public float speed = 5f; // �Ѿ� �ӵ�

    void Start()
    {
        // �÷��̾� ���� ���
        dir = (GameManager.Instance._playerTrm.position - transform.position).normalized;
    }

    void Update()
    {
        // �����̱�
        transform.Translate(dir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHp>().OnDamage(10);
            print("��ž�� �Ѿ˷� ���� 10�� �������� �����ϴ�.");
            Destroy(gameObject);
        }
    }
}
