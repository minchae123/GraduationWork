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
        CheckMouseRelease();
    }

    //������Ʈ �Ǻ����ִ� �ݶ��̴� ��ġ
    private void ColDir()
    {
        if (_direction.Direction())
            _boxCol.offset = new Vector2(-3, 0);
        else
            _boxCol.offset = new Vector2(3, 0);
    }

    //�ݶ��̴��� ������ �� û�ұ⿡ ���� ���� 
    private void OnTriggerStay2D(Collider2D collision)
    {
        _isMouseClick = Input.GetMouseButton(0);
        if (!collision.GetComponent<DivideObj>() || !_isMouseClick) return; //���� ������Ʈ�� DivideObj��ũ��Ʈ�� ������ ���� �ʰų� ���콺 ��ư�� �������ʾ��� �� ���� ����

        float dis = Vector2.Distance(collision.transform.position, _gatherPos.position); //���� ������Ʈ�� û�ұ� �Ա� ��ġ�� �Ÿ�
        float _dirTime = Mathf.Lerp(_minSpeed, _maxSpeed, Mathf.InverseLerp(0f, 10f, dis)); //�Ÿ��� ���ص� �ּ� ���� �ִ� �� ���̿��� �̷����������ؼ� �Ÿ��� �ָ� �ӵ��� ������ �ǰ�

        collision.transform.DOMove(_gatherPos.position, _dirTime / _speed / 1.5f); //û�ұ� ���� ��ġ���� �ٰ�����

        float targetScale = Mathf.Lerp(1f, 0f, Mathf.InverseLerp(0f, 20f, dis)); 

        DotScale(collision.gameObject, targetScale);
    }

    private void DotScale(GameObject obj, float targetScale)
    {
        _cleaningSequence = DOTween.Sequence(); //�ʱ�ȭ

        _cleaningSequence.Append(obj.transform.DOScale(0, targetScale / 1.5f)); //ũ�� �������ֱ�
        _cleaningSequence.OnComplete(() => Destroy(obj)); //��Ʈ���� �� ����Ǹ� �������
    }

    private void CheckMouseRelease()
    {
        if (Input.GetMouseButtonUp(0) && _cleaningSequence.IsActive())
            _cleaningSequence.Kill(); 
    }
}
