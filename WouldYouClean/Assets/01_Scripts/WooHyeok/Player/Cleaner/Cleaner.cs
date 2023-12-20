using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Cleaner : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _gatherPos;
    [SerializeField] private PlayerDirection _direction;
    [SerializeField] private BoxCollider2D _boxCol;

    [Header("시간")]
    [SerializeField] private float _speed;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;

    private Sequence _cleaningSequence;
    private bool _isMouseClick;

    private void Update()
    {
        ColDir();
        CleanAnim();
        CheckMouseRelease();
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
        if (!collision.GetComponent<DivideObj>() || !_isMouseClick) return;

        float dis = Vector2.Distance(collision.transform.position, _gatherPos.position);
        float _dirTime = Mathf.Lerp(_minSpeed, _maxSpeed, Mathf.InverseLerp(0f, 10f, dis));

        collision.transform.DOMove(_gatherPos.position, _dirTime / _speed / 1.5f);

        float targetScale = Mathf.Lerp(1f, 0f, Mathf.InverseLerp(0f, 20f, dis));

        DotScale(collision.gameObject, targetScale);
    }

    private void CleanAnim()
    {
        _isMouseClick = Input.GetMouseButton(0);
        _animator.SetBool("clean", _isMouseClick);
    }

    private void DotScale(GameObject obj, float targetScale)
    {
        _cleaningSequence = DOTween.Sequence();

        _cleaningSequence.Append(obj.transform.DOScale(0, targetScale / 1.5f));
        _cleaningSequence.OnComplete(() => Destroy(obj));
    }

    private void CheckMouseRelease()
    {
        if (Input.GetMouseButtonUp(0) && _cleaningSequence.IsActive())
            _cleaningSequence.Kill();
    }
}
