using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ToggleButton : MonoBehaviour, Item
{
    [Header("나중에 리스트로 변경")]
    [SerializeField] private Door connectDoor; //이어진 문 넣어주기
    [SerializeField] private List<Door> connectDoorList; //이어진 문 넣어주기

    private bool isClick = false;

    public void Rotation(bool value)
    {
        Vector3 rot = value ? new Vector3(0f, 0f, 180f) : Vector3.zero;
        transform.rotation = Quaternion.Euler(rot);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!StageManager.Instance.IsInStage) return;

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
        connectDoorList.ForEach(x => x.Close());

        isClick = false;

        transform.DOScaleY(0.25f, 0.5f);
        transform.localPosition = new Vector3(transform.localPosition.x, -0.3f, transform.localPosition.z);
    }
    public void DoorOpen()
    {
        connectDoor.Open();
        connectDoorList.ForEach(x => x.Open());

        isClick = true;

        transform.DOScaleY(0.1f, 0.5f);
        transform.localPosition = new Vector3(transform.localPosition.x, -0.45f, transform.localPosition.z);
    }
}
