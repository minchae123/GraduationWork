using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlienMain;

public class FindPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnFollow(collision, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnFollow(collision, false);
    }

    private void OnFollow(Collider2D col, bool value)
    {
        if (col.TryGetComponent<PlayerMain>(out PlayerMain player))
        {
            Alien._isFollow = value;
        }
    }
}
