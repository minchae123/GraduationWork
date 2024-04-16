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
            print("ddd");
        if (teleportManager != null)
        {
            print("Ddd");
            if (other.CompareTag("Teleport"))
            {
                print(teleportManager.tpPair[other.transform]); // �̵��� ��ġ
                transform.position = teleportManager.tpPair[transform].position;
                isTP = true;
            }
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Teleport") && isTP)
    //    {
    //        print("������");
    //        isTP = false;
    //    }
    //}
}
