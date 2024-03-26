using System.Collections;
using UnityEngine;

public class Turrets : MonoBehaviour//////////////////////////////////////사용하지않는스크립트입니다람쥐처럼볼에다도토리채우기
{
    public float attackRange = 5;
    public GameObject Bullet;
    private bool isShooting;

    private void Start()
    {
        isShooting = false;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, GameManager.Instance._playerTrm.position) < attackRange)
        {
            print(true);
            if (!isShooting)
            {
                print(false);
                isShooting = true;
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(3f);

        for (int i = 0; i < 5; i++)
        {
            //총알발싸두두두두두두두두두두두두두
            if (isShooting)
            {
                Instantiate(Bullet, Vector3.zero, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
            }
        }
        isShooting = false;
    }
}
