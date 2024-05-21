using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ToggleButton : MonoBehaviour, Item
{
    [SerializeField] private Door connectDoor; //이어진 문 넣어주기

    private bool isClick = false;

    public void Init()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(isClick) // 누른 적이 있는데 다시 눌렀을 경우
            {
                DoorClose();
            }
            else // 반대
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
