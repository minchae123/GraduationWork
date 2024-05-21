using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinderButton : MonoBehaviour
{
    [SerializeField] Material AppearMat;
    [SerializeField] Material outLineMat;


    // ����Ʈ�� �ִ°� ���������� ���̰� �Ѵ�.
    [SerializeField] List<HiddenPath> hiddenPaths = new List<HiddenPath>();
    Queue<GameObject> meshs = new Queue<GameObject>();

    /// <summary>
    /// //////////////////////ť�� �־ �ϳ��� ���鼭 ���� �ٲ� ����
    /// </summary>
    private void Awake()
    {
        foreach (var path in hiddenPaths)
        {
            path.gameObject.SetActive(false);
            meshs.Enqueue(path.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            ColorChange();
        }
    }


    private void ColorChange()
    {
        if (meshs.Count > 0)
        {
            print("change");
            GameObject mesh = meshs.Dequeue();
            mesh.SetActive(true);
            mesh.gameObject.layer = 6; // moveable ������ �� �ְ�
            mesh.GetComponent<MeshRenderer>().material.DOColor(AppearMat.color, 1f).onComplete += ColorChange;
        }
    }
}
