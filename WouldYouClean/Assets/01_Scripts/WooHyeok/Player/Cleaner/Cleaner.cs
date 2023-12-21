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
        CheckMouseRelease();
    }

    //오브젝트 판별해주는 콜라이더 위치
    private void ColDir()
    {
        if (_direction.Direction())
        {
            _boxCol.offset = new Vector2(-3, 0);
            _gatherPos.position = new Vector2(-1.15f, _gatherPos.position.y);
        }
        else
        {
            _boxCol.offset = new Vector2(3, 0);
            _gatherPos.position = new Vector2(1.15f, _gatherPos.position.y);
        }
    }

    //콜라이더에 들어왔을 때 청소기에 빨려 들어가게 
    private void OnTriggerStay2D(Collider2D collision)
    {
        _isMouseClick = Input.GetMouseButton(0);
        if (!collision.TryGetComponent<DivideObj>(out DivideObj obj) || !_isMouseClick) return; //들어온 오브젝트가 DivideObj스크립트를 가지고 있지 않거나 마우스 버튼이 눌리지않았을 때 실행 안함

        float dis = Vector2.Distance(collision.transform.position, _gatherPos.position); //들어온 오브젝트와 청소기 입구 위치의 거리
        float _dirTime = Mathf.Lerp(_minSpeed, _maxSpeed, Mathf.InverseLerp(0f, 10f, dis)); //거리를 정해둔 최소 값과 최대 값 사이에서 이러쿵저러쿵해서 거리가 멀면 속도가 빠르게 되게

        collision.transform.DOMove(_gatherPos.position, _dirTime / _speed).SetEase(Ease.OutQuad); //청소기 윕구 위치까지 다가가게

        float targetScale = Mathf.Lerp(1f, 0f, Mathf.InverseLerp(0f, 20f, dis));

        DotScale(collision.gameObject, targetScale, obj);
    }

    private void DotScale(GameObject obj, float targetScale, DivideObj divObj)
    {
        _cleaningSequence = DOTween.Sequence(); //초기화

        _cleaningSequence.Append(obj.transform.DOScale(0, targetScale)).SetEase(Ease.OutQuad); //크기 조절해주기
        _cleaningSequence.OnComplete(() =>
        {
            GameManager.instance._items[divObj.name]++;
            Destroy(obj);
        }); //다트윈이 다 실행되면 사라지게
    }

    private void CheckMouseRelease()
    {
        if (Input.GetMouseButtonUp(0) && _cleaningSequence.IsActive())
            _cleaningSequence.Kill();
    }
}
