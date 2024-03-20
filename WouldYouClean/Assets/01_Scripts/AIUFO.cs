using DG.Tweening;
using UnityEngine;

public class AIUFO : SpaceObject
{
    public float speed;
    Vector2 direction;
    SpaceShip spaceShip;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        spaceShip = FindAnyObjectByType<SpaceShip>();
    }

    private void Update()
    {
        Vector2 movement = transform.up * speed;
        //transform.position += new Vector3(movement.x, movement.y) * Time.deltaTime;

        if (Vector3.Distance(_rb.position, GameManager.Instance.SpaceShipTrm.position) < 30)//30���� ������ �޷����
        {
            SetDir(-spaceShip.transform.up * spaceShip.curSpeed, movement);
        }
        else //30���� �ָ� �׳� ���ٴϱ�
        {
            SetDir(-spaceShip.transform.up * spaceShip.curSpeed);
        }
        direction = (Vector2)GameManager.Instance.SpaceShipTrm.position - _rb.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;


        DOVirtual.Float(_rb.rotation, targetAngle, 0.5f, angle => _rb.rotation = angle);
    }
}
