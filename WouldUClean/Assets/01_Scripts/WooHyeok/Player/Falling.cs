using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    [SerializeField] private float minFallDamage = 10f;
    [SerializeField] private float respawnDamage = 50f;
    [SerializeField] private float fallingTime = 5f;
    [SerializeField] private float damage = 5f;

    private PlayerHp _hp;

    private bool isFall = false;
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
        if (SpaceManager.Instance.isSpace) return;

        if (IsFall())
        {
            FallDamage(maxHigh < minFallDamage);

            return;
        }

        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, 100);

        if(hit.collider == null && !isFall)
        {
            isFall = true;
            StartCoroutine(ReStart());
        }

        float high = hit.distance;
        maxHigh = maxHigh > high ? maxHigh : high;
    }

    IEnumerator ReStart()
    {
        yield return new WaitForSeconds(fallingTime);

        _hp.OnDamage(respawnDamage);
        transform.position = SpaceManager.Instance._spaceshipPos.position;
        maxHigh = 0;

        yield return new WaitForSeconds(0.5f);
        isFall = false;
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
        Physics.Raycast(transform.position, Vector3.down, 1f);
}
