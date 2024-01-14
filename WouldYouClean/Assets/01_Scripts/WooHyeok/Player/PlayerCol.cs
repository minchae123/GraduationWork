using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCol : PlayerMain
{
    private PlayerHp _playerHp;

    private void Awake()
    {
        _playerHp = GetComponent<PlayerHp>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Meteor"))
        {
            _playerHp.OnDamage(20);
            Destroy(collision.gameObject);
        }
    }
}
