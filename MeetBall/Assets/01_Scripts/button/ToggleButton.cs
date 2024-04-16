using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] private Door connectDoor; //이어진 문 넣어주기

    private bool isClick = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(isClick) // 누른 적이 있는데 다시 눌렀을 경우
            {
                connectDoor.Close();
                isClick = false;
            }
            else // 반대
            {
                connectDoor.Open();
                isClick = true;
            }
        }
    }
}
