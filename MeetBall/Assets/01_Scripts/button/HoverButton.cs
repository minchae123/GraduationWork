﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverButton : MonoBehaviour, Item
{
    [SerializeField] private List<Door> connectDoor; //이어진 문 넣어주기

    public void Init()
    {

    }

	private void OnTriggerStay(Collider other)
    {
        if (!StageManager.Instance.IsInStage) return;
        if(connectDoor.Count > 0)
        {
            if (!other.CompareTag("Moveable"))
            {
                connectDoor.ForEach(door => { door.Open(); }); //우와우와와오아ㅗ아우왕와와와왕우와우ㅏ와우아와와와왕
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        connectDoor.ForEach(door => { door.Close(); });
    }
}