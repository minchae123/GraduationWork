using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] private Door connectDoor; //�̾��� �� �־��ֱ�

    private bool isClick = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(isClick) // ���� ���� �ִµ� �ٽ� ������ ���
            {
                connectDoor.Close();
                isClick = false;
            }
            else // �ݴ�
            {
                connectDoor.Open();
                isClick = true;
            }
        }
    }
}
