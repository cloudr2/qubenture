using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IHitteable
{
    [SerializeField]
    private float maxHealth;
    private float currentHealth;

    public float CurrentHealth { get { return currentHealth; } }
    public event Action OnDeath = delegate () { };
    public event Action OnHit = delegate () { };

    void Start() {
        Initialize();
    }

    private void Initialize()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        OnHit();
        CheckHealth();
    }

    public void RegainHealth(float heal) {
        if (currentHealth + heal >= maxHealth)
            currentHealth = maxHealth;
        else currentHealth += heal;
    }

    public void CheckHealth() {
        if (currentHealth <= 0)
            OnDeath();
    }
}
