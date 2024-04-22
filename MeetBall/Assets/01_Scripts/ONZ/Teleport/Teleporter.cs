﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] float _tpDelayTime;

    TeleportManager teleportManager;

    bool isTP = false; // ������ ������

    private void Awake()
    {
        teleportManager = transform.parent.GetComponentInChildren<TeleportManager>();
    }
    private void Start()
    {
        if (_tpDelayTime == 0)
            Debug.LogError("텔포딜레이가 0이면 아주 심각한 문제가 발생합니다람쥐");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (teleportManager != null)
        {
            if (other.CompareTag("Teleport") && !isTP)
            {
                if (teleportManager.tpPair.ContainsKey(other.transform))
                {
                    isTP = true;
                    print(isTP);
                    print(teleportManager.tpPair[other.transform]); // �̵��� ��ġ
                    transform.position = teleportManager.tpPair[other.transform].position;
                }
                else
                {
                    print("탈수없는것임.");
                }
                CoroutineUtil.CallWaitForSeconds(_tpDelayTime, null, () => isTP = false);
            }
        }
    }
}