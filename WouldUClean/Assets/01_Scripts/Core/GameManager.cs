using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Dictionary<string, int> _items;
    public Transform _playerTrm;
    public Transform SpaceShipTrm;
    public Camera mainCam;
    public PoolingListSO poolingListSO;


    // Start is called before the first frame update
    void Awake()
    {
        mainCam = Camera.main;
        MakePool();
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform);
        poolingListSO.list.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount));
    }



    // Update is called once per frame
    void Update()
    {

    }
}
