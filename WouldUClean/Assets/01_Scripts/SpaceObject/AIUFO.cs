using DG.Tweening;
using System.Collections;
using UnityEngine;

public class AIUFO : SpaceObject
{
    public float speed;
    Vector2 direction;
    SpaceShip spaceShip;
    private float rotateSpeed = 10;
    private bool isChoosingState = false; // 상태를 선택 중인지 여부를 나타내는 플래그

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        spaceShip = FindAnyObjectByType<SpaceShip>();
    }

    private void Update()
    {
        Vector2 movement = transform.up * speed;
        SetDir(-spaceShip.transform.up * spaceShip.curSpeed, movement);

        if (Vector3.Distance(_rb.position, GameManager.Instance.SpaceShipTrm.position) < 30)//30보다 가까우면 달려들고
        {
            direction = (Vector2)GameManager.Instance.SpaceShipTrm.position - _rb.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            DOVirtual.Float(_rb.rotation, targetAngle, 0.5f, angle => _rb.rotation = angle);
        }
        else if (Vector3.Distance(_rb.position, GameManager.Instance.SpaceShipTrm.position) < 200)
        {
            if (!isChoosingState)
            {
                StartCoroutine(ChooseState());
            }
            transform.Rotate(0, 0, Time.deltaTime * rotateSpeed);
        }
        else
        {
            direction = (Vector2)GameManager.Instance.SpaceShipTrm.position - _rb.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            DOVirtual.Float(_rb.rotation, targetAngle, 0.5f, angle => _rb.rotation = angle);
        }
    }

    private IEnumerator ChooseState()
    {
        isChoosingState = true;

        int r = Random.Range(0, 3);
        int r2 = Random.Range(10, 30);
        if (r == 0)
        {
            rotateSpeed = 0;
        }
        else if (r == 1)
        {
            rotateSpeed = -r2;
        }
        else if (r == 2)
        {
            rotateSpeed = r2;
        }
        yield return new WaitForSeconds(2f);

        isChoosingState = false;
    }
}
