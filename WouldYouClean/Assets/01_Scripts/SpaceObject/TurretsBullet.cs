using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretsBullet : MonoBehaviour
{
    Vector3 dir;
    public float speed = 5f; // 총알 속도

    void Start()
    {
        // 플레이어 방향 계산
        dir = (GameManager.Instance._playerTrm.position - transform.position).normalized;
    }

    void Update()
    {
        // 움직이기
        transform.Translate(dir * speed * Time.deltaTime);
    }


}
