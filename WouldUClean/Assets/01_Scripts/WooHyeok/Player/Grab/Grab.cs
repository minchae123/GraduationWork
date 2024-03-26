using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grab : MonoBehaviour
{
    [SerializeField] private Transform colPos;
    [SerializeField] private float _speed;

    private bool _isGrabbing = false;

    public void GrabTrash(DivideObj obj)
    {
        StartCoroutine(GrabMotion(obj));
    }

    IEnumerator GrabMotion(DivideObj obj)
    {
        Vector3 size = Vector3.zero;
        size.z = _speed;

        while (true)
        {
            if (_isGrabbing)
            {
                transform.localScale -= size * Time.deltaTime;

                if (CatchObj(obj))
                {
                    transform.localScale = Vector3.one;
                    print("ss");
                    break;
                }
            }
            else
            {
                transform.localScale += size * Time.deltaTime;

                _isGrabbing = IsStop(obj);
            }

            yield return null;
        }
    }

    private bool CatchObj(DivideObj obj)
    {
        if (obj == null) return true;

        float dis = Vector3.Distance(transform.position, obj.transform.position) / (_speed / 4);
        bool answer = false;

        obj.transform.DOMove(colPos.position, dis);
        obj.transform.DOScale(Vector3.zero, dis)
            .OnComplete(() =>
            {
                CollectedPlanets.Instance.AddTrashCollected(obj);//도감에 추가
                obj.PickUpItem();

                _isGrabbing = false;
                answer = true;
            });

        return answer;
    }

    private bool IsStop(DivideObj obj)
    {
        if (obj == null) return true;

        Vector3 dir = obj.transform.position - colPos.position;
        return Physics.Raycast(colPos.position, dir, 1f, LayerMask.GetMask("Trash"));
    }
}
