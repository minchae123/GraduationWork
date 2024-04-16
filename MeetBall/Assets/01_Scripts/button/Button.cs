using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Door connectDoor; //이어진 문 넣어주기

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Moveable"))
        {
            connectDoor.Open();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        connectDoor.Close();
    }
}