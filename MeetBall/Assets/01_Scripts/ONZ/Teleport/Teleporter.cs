using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private float _tpDelayTime = .5f;

    TeleportManager teleportManager;

    bool isTP = false; // ������ ������

    private void Awake()
    {
        //_tpDelayTime = 0.1f
    }

    private void Start()
    {
        teleportManager = StageManager.Instance.StageTrm.GetComponentInChildren<TeleportManager>();

        if (_tpDelayTime == 0)
            Debug.LogError("텔포딜레이가 0이면 아주 심각한 문제가 발생합니다람쥐");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (teleportManager != null)
        {
            print(0);
            if (other.CompareTag("Teleport") && !isTP)
            {
                if (teleportManager.tpPair.ContainsKey(other.transform))
                {
                    isTP = true;
                    //print(teleportManager.tpPair[other.transform]); // �̵��� ��ġ
                    StartCoroutine(Teleporting(teleportManager.tpPair[other.transform]));
                    //transform.position = teleportManager.tpPair[other.transform].position;
                }
                else
                {
                    print("탈수없는것임.");
                }
                CoroutineUtil.CallWaitForSeconds(_tpDelayTime + .1f, null, () => isTP = false);
            }
        }
    }

    private IEnumerator Teleporting(Transform tpPos)
    {
        transform.DOScale(0, _tpDelayTime);
        yield return new WaitForSeconds(_tpDelayTime);
        transform.position = tpPos.position;
        transform.DOScale(1, _tpDelayTime);
    }
}