using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IHitteable
{
    [SerializeField]
    private float maxHealth;
    private float currentHealth;

    public float CurrentHealth { get { return currentHealth; } }
    public float MaxHealth { get { return maxHealth; } }

    public event Action OnDeath = delegate () { };
    public event Action OnHit = delegate () { };

    public bool isInvincible { get; set; }
    public bool isAlive { get { return currentHealth > 0 ? true : false; }  }

    void Start() {
        Initialize();
    }

    private void Initialize()
    {
        currentHealth = maxHealth;
        isInvincible = false;
    }

    public void TakeDamage(float damage) {
        if (!isInvincible)
        {
            GameManager.Instance.PlaySFX(GameManager.Instance.hitSFX);
            currentHealth -= damage;
            OnHit();
            CheckHealth();
        }
    }

    public void RegainHealth(float heal) {
        if (currentHealth + heal >= maxHealth)
            currentHealth = maxHealth;
        else currentHealth += heal;
    }

    public void CheckHealth() {
        if (currentHealth <= 0) {
            GameManager.Instance.PlaySFX(GameManager.Instance.deathSFX);
            OnDeath();
        }
    }
}
