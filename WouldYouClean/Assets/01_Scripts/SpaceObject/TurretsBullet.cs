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


}
