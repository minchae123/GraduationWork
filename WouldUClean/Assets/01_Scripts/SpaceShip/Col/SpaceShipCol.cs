using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipCol : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHp>(out PlayerHp player))
            player._isPlain = true;
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHp>(out PlayerHp player))
            player._isPlain = true;
    }
}
