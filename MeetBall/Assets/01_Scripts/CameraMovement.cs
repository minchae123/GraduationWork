using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraMovement : MonoSingleton<CameraMovement>
{
    public List<Transform> transforms = new List<Transform>();
    public CinemachineVirtualCamera[] cinemachineCam;

    private CinemachineVirtualCamera previousCam;
    public Transform curTransfrom;

    public HoverButton button { get; set; }
    public Direction _dir;

    public int left, up;

    private List<Transform> items;

    private void Start()
    {
        previousCam = cinemachineCam[3];
    }

    public void FindItems()
    {
        items = GameManager.Instance.FindAllItems();
    }

    public void ChangeCamera(Direction dir)
    {
        bool isTurn = false;
        FindMovement();

        foreach (var c in cinemachineCam)
        {
            if (c.Priority == 10)
                previousCam = c;
        }

        switch (dir)
        {
            case Direction.Left:
                {
                    left++;
                    int i = left % 4;
                    if (i == 0) i = 4;
                    curTransfrom = cinemachineCam[i - 1].transform;
                    cinemachineCam[i - 1].Priority = 10;
                }
                break;
            case Direction.Right:
                {
                    left--;
                    if (left == -1) left = 3;
                    int i = left % 4;
                    if (i == 0) i = 4;
                    curTransfrom = cinemachineCam[i - 1].transform;
                    cinemachineCam[i - 1].Priority = 10;
                }
                break;
            case Direction.Up:
                {
                    if (up > 0)
                    {
                        up = 0;
                        int i = left % 4;
                        if (i == 0) i = 4;
                        curTransfrom = cinemachineCam[i - 1].transform;
                        cinemachineCam[i - 1].Priority = 10;
                    }
                    else
                    {
                        up++;

                        curTransfrom = cinemachineCam[4].transform;
                        cinemachineCam[4].Priority = 10;
                    }
                }
                break;
            case Direction.Down:
                {
                    isTurn = true;

                    if (up < 0)
                    {
                        up = 0;
                        int i = left % 4;
                        if (i == 0) i = 4;
                        curTransfrom = cinemachineCam[i - 1].transform;
                        cinemachineCam[i - 1].Priority = 10;
                    }
                    else
                    {
                        up--;

                        curTransfrom = cinemachineCam[5].transform;
                        cinemachineCam[5].Priority = 10;
                    }
                }
                break;
        }

        ItemRot(isTurn);
        up = Mathf.Clamp(up, -1, 1);

        previousCam.Priority = 5;
        _dir = dir;
    }

    private void ItemRot(bool value)
    {
        foreach (Transform item in items)
        {
            Vector3 rot = value ? new Vector3(0f, 0f, 180f) : Vector3.zero;
            item.transform.Rotate(rot);
        }
    }

    private void FindMovement()
    {
        Movement[] movement = FindObjectsOfType<Movement>();

        foreach (Movement m in movement)
            m.RayCheck();
    }
}