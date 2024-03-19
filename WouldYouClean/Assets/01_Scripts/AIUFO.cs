using DG.Tweening;
using UnityEngine;

public class AIUFO : SpaceObject
{
    public float speed;
    Vector2 direction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 movement = transform.up * speed;
        transform.position += new Vector3(movement.x, movement.y) * Time.deltaTime;

        direction = (Vector2)GameManager.Instance.SpaceShipTrm.position - _rb.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Tweening�� ����Ͽ� �ε巴�� ȸ���ϵ��� �մϴ�.
        DOVirtual.Float(_rb.rotation, targetAngle, 0.5f, angle => _rb.rotation = angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spaceship"))
        {
            print("��������.����Ʈ���̰����͹з����°͵��߰��ϱ�");
            SetDir(Vector2.up * 100);
            print(new Vector2(direction.x + 1, direction.y + 1) * 100);
            //gameObject.SetActive();
        }
    }
}
