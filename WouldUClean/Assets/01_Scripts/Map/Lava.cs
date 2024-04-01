using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerHp>(out PlayerHp hp))
        {
            Debug.Log("asdf");
            hp.OnDamage(1f);
        }
    }
}
