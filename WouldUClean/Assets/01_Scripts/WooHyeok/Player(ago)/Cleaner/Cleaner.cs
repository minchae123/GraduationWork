using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Cleaner : MonoBehaviour
{
    [Header("����")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _gatherPos;
    [SerializeField] private PlayerDirection _direction;
    [SerializeField] private ItemSlotUI _slot;
    [SerializeField] private BoxCollider2D _boxCol;

    [Header("�ð�")]
    [SerializeField] private float _speed;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;

    private Sequence _cleaningSequence;
    private bool _isMouseClick;

    private void Update()
    {
        ColDir();
    }

    //������Ʈ �Ǻ����ִ� �ݶ��̴� ��ġ
    private void ColDir()
    {
        if (_direction.Direction())
        {
            _boxCol.offset = new Vector2(3, 0);
            _gatherPos.position = new Vector2(transform.position.x + 1.15f, _gatherPos.position.y);
        }
        else
        {
            _boxCol.offset = new Vector2(-3, 0);
            _gatherPos.position = new Vector2(transform.position.x - 1.15f, _gatherPos.position.y);
        }
    }

    //�ݶ��̴��� ������ �� û�ұ⿡ ���� ���� 
    private void OnTriggerStay2D(Collider2D collision)
    {
        _isMouseClick = Input.GetMouseButtonDown(0);

        if (_slot._isDragging)
            _isMouseClick = false;

        if (!collision.TryGetComponent<DivideObj>(out DivideObj obj)
              && !collision.TryGetComponent<AlienMovement>(out AlienMovement alien) || !_isMouseClick) return; //���� ������Ʈ�� DivideObj��ũ��Ʈ�� ������ ���� �ʰų� ���콺 ��ư�� �������ʾ��� �� ���� ����

        Transform trm = collision.transform;

        if (collision.transform.parent != null)
            trm = collision.transform.parent;

        float dis = Vector2.Distance(trm.position, _gatherPos.position); //���� ������Ʈ�� û�ұ� �Ա� ��ġ�� �Ÿ�
        float _dirTime = Mathf.Lerp(_minSpeed, _maxSpeed, Mathf.InverseLerp(0f, 10f, dis)); //�Ÿ��� ���ص� �ּ� ���� �ִ� �� ���̿��� �̷����������ؼ� �Ÿ��� �ָ� �ӵ��� ������ �ǰ�

        trm.DOMove(_gatherPos.position, _dirTime / _speed).SetEase(Ease.OutQuad); //û�ұ� ���� ��ġ���� �ٰ�����

        float targetScale = Mathf.Lerp(1f, 0f, Mathf.InverseLerp(0f, 20f, dis));

        DotScale(trm, targetScale, obj);

        MapManager.Instance.UpdateTrashList();

    }

    private void DotScale(Transform obj, float targetScale, DivideObj divObj)
    {
        _cleaningSequence = DOTween.Sequence(); //�ʱ�ȭ

        _cleaningSequence.Append(obj.DOScale(0, targetScale)).SetEase(Ease.OutQuad); //ũ�� �������ֱ�
        _cleaningSequence.OnComplete(() =>
        {
            bool isAlien = false;

            foreach (Transform child in obj)
            {
                if (child.TryGetComponent<AlienMovement>(out AlienMovement aliens))
                {
                    aliens.ReChargingList();
                    isAlien = true;
                }
            }

            if (!isAlien && obj.localScale == Vector3.zero)
            {
                CollectedPlanets.Instance.AddTrashCollected(divObj);//������ �߰�
                divObj.PickUpItem();
            }
        }); //��Ʈ���� �� ����Ǹ� �������
    }
}