using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cleaner : MonoBehaviour
{
    [SerializeField] private Transform _gatherPos;

    [Header("참조")]
    [SerializeField] private PlayerDirection _direction;
    [SerializeField] private BoxCollider2D _boxCol;

    [Header("시간")]
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;


    private void Update()
    {
        ColDir();
    }

    private void ColDir()
    {
        if (_direction.Direction())
            _boxCol.offset = new Vector2(-3, 0);
        else
            _boxCol.offset = new Vector2(3, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.GetComponent<DivideObj>()) return;

        float dis = Vector2.Distance(collision.transform.position, _gatherPos.position);
        float _dirTime = Mathf.Lerp(_minSpeed, _maxSpeed, Mathf.InverseLerp(0f, 10f, dis));

        Vector2 dir = Vector2.MoveTowards(collision.transform.position, _gatherPos.position, _dirTime * Time.deltaTime);

        collision.transform.position = dir;

        float targetScale = Mathf.Lerp(1f, 0f, Mathf.InverseLerp(0f, 20f, dis));
        collision.transform.DOScale(0, targetScale).OnComplete(()=>Destroy(collision.gameObject));
    }
}
