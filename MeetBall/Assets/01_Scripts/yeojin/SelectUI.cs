using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SelectUI : MonoBehaviour
{
    private Vector3 originPos;
    private Vector3 selectPos;

    [SerializeField] private float time = 0.5f;

    private void Awake()
    {
        originPos = Vector3.zero;
        selectPos = transform.localPosition;
    }

    private void Start()
    {
        transform.localPosition = originPos;
    }

    public void MoveToOrigin()
    {
        transform.DOLocalMove(originPos, time).SetEase(Ease.OutQuad);
    }
    public void MoveToSelect()
    {
        transform.DOLocalMove(selectPos, time).SetEase(Ease.OutQuad);
    }
}
