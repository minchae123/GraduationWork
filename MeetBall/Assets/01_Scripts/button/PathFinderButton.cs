using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinderButton : MonoBehaviour
{
    [SerializeField] Material AppearMat;
    [SerializeField] Material outLineMat;


    // 리스트에 있는걸 순차적으로 보이게 한다.
    [SerializeField] List<HiddenPath> hiddenPaths = new List<HiddenPath>();
    Queue<GameObject> meshs = new Queue<GameObject>();

    /// <summary>
    /// //////////////////////큐에 넣어서 하나씩 빼면서 색을 바꿀 예정
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
            mesh.gameObject.layer = 6; // moveable 움직일 수 있게
            mesh.GetComponent<MeshRenderer>().material.DOColor(AppearMat.color, 1f).onComplete += ColorChange;
        }
    }
}
