using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grab : MonoBehaviour
{
    [SerializeField] private Transform _colPos;
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;

    private RaycastHit _hit;

    public bool _isTurning = true;
    private bool _isGrabbing = false;
    private bool _isShrink = true;

    private float _length = 20f;
    private float _time = 0.5f;

    //public void GrabTrash(DivideObj obj)
    //{
    //    _isGrabbing = false;
    //    StartCoroutine(GrabMotion(obj));
    //}
    //IEnumerator GrabMotion(DivideObj obj)
    //{
    //    Vector3 size = Vector3.zero;
    //    size.z = _speed;

    //    _isTurning = false;

    //    while (true)
    //    {
    //        if (_isGrabbing)
    //        {
    //            transform.localScale -= size * Time.deltaTime;

    //            if (CatchObj(obj))
    //            {
    //                transform.localScale = Vector3.one;
    //                break;
    //            }
    //        }
    //        else
    //        {
    //            transform.localScale += size * Time.deltaTime;

    //            _isGrabbing = IsStop(obj);
    //        }

    //        RotGrab();

    //        yield return null;
    //    }

    //    _isTurning = true;
    //}

    public void EmptyGrab()
    {
        transform.DOScaleZ(_length, _time)
            .OnUpdate(() =>
            {
                if (FindTrash())
                    if (_hit.transform.TryGetComponent<DivideObj>(out DivideObj obj))
                    {
                        _isShrink = false;
                        transform.DOScaleZ(1, _time);

                        CatchObj(obj);
                    }

                RotGrab();
            })
            .OnComplete(() =>
            {
                if (_isShrink)
                    transform.DOScaleZ(1, _time);
            });

        _isShrink = true;
    }

    private bool CatchObj(DivideObj obj)
    {
        if (obj == null) return true;

        float dis = Vector3.Distance(transform.position, obj.transform.position) / (_speed / 4);
        bool answer = false;

        obj.transform.DOMove(_colPos.position, dis);
        obj.transform.DOScale(Vector3.zero, dis)
            .OnComplete(() =>
            {
                AddInven(obj);

                answer = true;
            });

        return answer;
    }

    private bool IsStop(DivideObj obj)
    {
        if (obj == null) return true;
        if (transform.localScale.z > 20) return true;

        Vector3 dir = obj.transform.position - _colPos.position;
        return FindTrash();
    }

    private void AddInven(DivideObj obj)
    {
        CollectedPlanets.Instance.AddTrashCollected(obj);//도감에 추가
        obj.PickUpItem();
    }
    private bool FindTrash()
    {
        return Physics.Raycast(_colPos.position, transform.forward, out _hit, 2f, LayerMask.GetMask("Trash"));
    }
    private void RotGrab()
    {
        transform.rotation = _player.rotation;
    }
}
