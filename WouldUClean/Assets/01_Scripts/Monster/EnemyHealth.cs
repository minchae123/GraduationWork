using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;



public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected int _currentHealth;

    public int MaxHealth;


    private HealthBarUI _healthBarUI;

    private void Awake()
    {
        _currentHealth = MaxHealth;
        _healthBarUI = transform.Find("HealthBar").GetComponent<HealthBarUI>();
        //_healthBarUI.SetHealth(MaxHealth);
    }


    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;


        _healthBarUI.SetHealth(_currentHealth);


        if (_currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        GetComponentInChildren<EnemyAnimator>().DeadTrigger(true);
        Destroy(gameObject, 1f);
    }
}
