using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ToggleButton : MonoBehaviour, Item
{
    [SerializeField] private Door connectDoor; //�̾��� �� �־��ֱ�

    private bool isClick = false;

    public void Init()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(isClick) // ���� ���� �ִµ� �ٽ� ������ ���
            {
                DoorClose();
            }
            else // �ݴ�
            {
                DoorOpen();
            }
        }
    }

    public void DoorClose()
    {
        connectDoor.Close();
        isClick = false;

        transform.DOScaleY(0.25f, 0.5f);
        transform.localPosition = new Vector3(transform.localPosition.x, -0.3f, transform.localPosition.z);
    }
    public void DoorOpen()
    {
        connectDoor.Open();
        isClick = true;

        transform.DOScaleY(0.1f, 0.5f);
        transform.localPosition = new Vector3(transform.localPosition.x, -0.45f, transform.localPosition.z);
    }
}
