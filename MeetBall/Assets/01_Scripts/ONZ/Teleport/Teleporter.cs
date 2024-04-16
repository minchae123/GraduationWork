using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    TeleportManager teleportManager;

    bool isTP = false; // ������ ������

    private void Awake()
    {
        teleportManager = transform.parent.GetComponent<TeleportManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (teleportManager != null)
        {
            if (other.CompareTag("Teleport") && !isTP)
            {
                isTP = true;
                print(isTP);
                print(teleportManager.tpPair[other.transform]); // �̵��� ��ġ
                transform.position = teleportManager.tpPair[other.transform].position;
                CoroutineUtil.CallWaitForSeconds(1f, null,() => isTP = false);
            }
        }
    }
}
