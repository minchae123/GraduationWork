using DG.Tweening;
using UnityEngine;

public class Billboard : MonoBehaviour // �̸��� ���̷����� �𸣰���
{
    Transform cam;
    private void Start()
    {
        cam = SpaceManager.Instance.MainCam.transform;
    }
    void Update()
    {
        transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
    }
}
