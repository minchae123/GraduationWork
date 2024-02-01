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
            _boxCol.offset = new Vector2(-3, 0);
            _gatherPos.position = new Vector2(transform.position.x - 1.15f, _gatherPos.position.y);
        }
        else
        {
            _boxCol.offset = new Vector2(3, 0);
            _gatherPos.position = new Vector2(transform.position.x + 1.15f, _gatherPos.position.y);
        }
    }

    //�ݶ��̴��� ������ �� û�ұ⿡ ���� ���� 
    private void OnTriggerStay2D(Collider2D collision)
    {
        _isMouseClick = Input.GetMouseButton(0);

        if (_slot._isDragging)
            _isMouseClick = false;

        if (!collision.TryGetComponent<DivideObj>(out DivideObj obj)
              && !collision.TryGetComponent<Alien>(out Alien alien) || !_isMouseClick) return; //���� ������Ʈ�� DivideObj��ũ��Ʈ�� ������ ���� �ʰų� ���콺 ��ư�� �������ʾ��� �� ���� ����

        float dis = Vector2.Distance(collision.transform.position, _gatherPos.position); //���� ������Ʈ�� û�ұ� �Ա� ��ġ�� �Ÿ�
        float _dirTime = Mathf.Lerp(_minSpeed, _maxSpeed, Mathf.InverseLerp(0f, 10f, dis)); //�Ÿ��� ���ص� �ּ� ���� �ִ� �� ���̿��� �̷����������ؼ� �Ÿ��� �ָ� �ӵ��� ������ �ǰ�

        collision.transform.DOMove(_gatherPos.position, _dirTime / _speed).SetEase(Ease.OutQuad); //û�ұ� ���� ��ġ���� �ٰ�����

        float targetScale = Mathf.Lerp(1f, 0f, Mathf.InverseLerp(0f, 20f, dis));

        DotScale(collision.gameObject, targetScale, obj);
        
        MapManager.Instance.UpdateTrashList();

    }

    private void DotScale(GameObject obj, float targetScale, DivideObj divObj)
    {
        _cleaningSequence = DOTween.Sequence(); //�ʱ�ȭ

        _cleaningSequence.Append(obj.transform.DOScale(0, targetScale)).SetEase(Ease.OutQuad); //ũ�� �������ֱ�
        _cleaningSequence.OnComplete(() =>
        {
            if (obj.TryGetComponent<Alien>(out Alien alien))
            {
                alien.ReChargingList();
            }
            else
            {

                if (obj.transform.localScale == Vector3.zero)
                {
                    CollectedPlanets.Instance.AddTrashCollected(divObj);//������ �߰�
                    divObj.PickUpItem();
                }
            }

            Destroy(obj);
        }); //��Ʈ���� �� ����Ǹ� �������
    }
}
