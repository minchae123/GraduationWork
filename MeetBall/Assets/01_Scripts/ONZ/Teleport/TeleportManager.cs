using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] List<Transform> StartPortalList = new List<Transform>();
    [SerializeField] List<Transform> EndPortalList = new List<Transform>();

    public Dictionary<Transform, Transform> tpPair = new Dictionary<Transform, Transform>();
    private void Awake()
    {
        for (int i = 0; i < StartPortalList.Count; ++i)
        {
            print("텔포초기화");
            tpPair.Add(StartPortalList[i], EndPortalList[i]);
        }
    }
}