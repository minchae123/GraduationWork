using DG.Tweening;
using UnityEngine;

public class AIUFO : SpaceObject
{
    public float speed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 movement = transform.up * speed;
        transform.position += new Vector3(movement.x, movement.y) * Time.deltaTime;

        Vector2 direction = (Vector2)GameManager.Instance.SpaceShipTrm.position - _rb.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Tweening�� ����Ͽ� �ε巴�� ȸ���ϵ��� �մϴ�.
        DOVirtual.Float(_rb.rotation, targetAngle, 0.5f, angle => _rb.rotation = angle);
    }
}
