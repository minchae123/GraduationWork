using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private float DMG;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerHp>(out PlayerHp hp))
        {
            hp.OnDamage(DMG);
        }
    }
}
