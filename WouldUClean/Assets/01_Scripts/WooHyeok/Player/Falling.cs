using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    [SerializeField] private float minFallDamage = 10f;
    [SerializeField] private float damage = 5f;

    private PlayerHp _hp;

    //private int layer = LayerMask.GetMask("Ground");
    private float maxHigh = 0f;

    private void Awake()
    {
        _hp = GetComponent<PlayerHp>();
    }

    private void Update()
    {
        CheckFalling();
    }

    private void CheckFalling()
    {
        if (IsFall())
        {
            FallDamage(maxHigh < minFallDamage);

            return;
        }

        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, LayerMask.GetMask("Ground"));

        float high = hit.distance;
        maxHigh = maxHigh > high ? maxHigh : high;
    }

    private void FallDamage(bool value)
    {
        if (value) return;  

        float fallDmg = damage * maxHigh;
        _hp.OnDamage(fallDmg);
        print(fallDmg);
        maxHigh = 0;
    }

    private bool IsFall() => 
        Physics.Raycast(transform.position, Vector3.down, 1f, LayerMask.GetMask("Ground"));
}
