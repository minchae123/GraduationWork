using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverButton : MonoBehaviour
{
    [SerializeField] private List<Door> connectDoor; //이어진 문 넣어주기

    private void OnTriggerStay(Collider other)
    {
        if(connectDoor.Count > 0)
        {
            if (!other.CompareTag("Moveable"))
            {
                connectDoor.ForEach(door => { door.Open(); });
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        connectDoor.ForEach(door => { door.Close(); });
    }
}