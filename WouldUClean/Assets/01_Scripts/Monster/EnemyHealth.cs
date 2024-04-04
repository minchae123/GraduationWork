using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;



public class EnemyHealth : MonoBehaviour, IDamageable
{
    protected int health;

    protected int _currentHealth;

    [SerializeField]
    protected int _maxHealth;


    private HealthBarUI _healthBarUI;

    private void Awake()
    {
        _healthBarUI = transform.Find("HealthBar").GetComponent<HealthBarUI>();
    }


    public void TakeDamage(int damage)
    {
        health -= damage;


        _healthBarUI.SetHealth(_currentHealth);


        if (health <= 0)
            Die();
    }

    private void Die()
    {
    }
}
