using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanRobot : MonoBehaviour
{
    [SerializeField]
    [Range(0, 30)] float _moveSpeed;
    [SerializeField]
    [Range(0, 5)] float _rotSpeed;
    [SerializeField] private LayerMask _targetLayer;

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Collider2D closestObject = FindClosestObject();

        if (closestObject != null)
        {
            MoveToTarget(closestObject.transform.position);

            Debug.Log("가장 근처에 있는 오브젝트: " + closestObject.gameObject.name);
        }
    }

    Collider2D FindClosestObject()
    {
        Collider2D[] allColliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity, _targetLayer);

        Collider2D closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D collider in allColliders)
        {
            float distance = Vector2.Distance(transform.position, collider.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = collider;
            }
        }

        return closestObject;
    }

    void MoveToTarget(Vector2 targetPosition)
    {
        Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

        float newRotation = Mathf.LerpAngle(_rb.rotation, angle - 90, Time.deltaTime * _rotSpeed);
        _rb.MoveRotation(newRotation);

        _rb.velocity = moveDirection * _moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<DivideObj>(out DivideObj obj))
        {
            CollectedPlanets.Instance.AddTrashCollected(obj);//도감에 추가
            obj.PickUpItem();
        }
    }
}
