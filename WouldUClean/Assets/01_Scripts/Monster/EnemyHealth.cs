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

    [SerializeField] private EnemyAnimator animator;

    private HealthBarUI _healthBarUI;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        animator = GetComponentInChildren<EnemyAnimator>();
        _healthBarUI = transform.Find("HealthBar").GetComponent<HealthBarUI>();
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        _healthBarUI.SetHealth(_currentHealth);
        animator.HitTrigger(true);

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        animator.DeadTrigger(true);
        Destroy(gameObject, 1f);
    }
}
