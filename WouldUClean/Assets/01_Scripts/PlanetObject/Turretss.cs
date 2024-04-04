using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turretss : MonoBehaviour
{
    [SerializeField] private Transform firePos;

    public bool isShooting = false;
    public float _shootRange; // start에서 초기화 했으니 주의하셈

    private RaycastHit hit;
    private Vector3 playerDirection;

    private void Start()
    {
        _shootRange = 10;
    }

    private void Update()
    {
        playerDirection = GameManager.Instance._playerTrm.position - transform.position;
        playerDirection.y = 0;
        if (Vector3.Distance(transform.position, GameManager.Instance._playerTrm.position) < _shootRange) // 범위에 들어오면 회전하기
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerDirection), 5 * Time.deltaTime);
        }

        if (Physics.Raycast(firePos.position, transform.forward, out hit, _shootRange)) // 보고있으면 쏘기
        {
            Debug.DrawRay(firePos.position, transform.forward * hit.distance, Color.red);
            if (hit.collider.CompareTag("Player") && !isShooting)
            {
                isShooting = true;
                StartCoroutine(Attack());
            }
        }
        else
        {
            Debug.DrawRay(firePos.position, transform.forward * 1000f, Color.red);
        }
    }

    private IEnumerator Attack()
    {

        for (int i = 0; i < 5; i++)
        {
            //총알발솨두두두두두더지렁이잡아먹기러기새우깡뺏기
            if (isShooting)
            {
                Bullet bullet = PoolManager.Instance.Pop("Bullet") as Bullet;
                bullet.transform.position = firePos.position;
                yield return new WaitForSeconds(0.2f);
            }
        }
        yield return new WaitForSeconds(3f);
        isShooting = false;
    }
}
