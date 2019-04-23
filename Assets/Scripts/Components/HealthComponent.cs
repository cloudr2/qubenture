using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    private float currentHealth;

    public float CurrentHealth { get { return currentHealth; } }

    void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        CheckHealth();
    }

    public void RegainHealth(float heal) {
        if (currentHealth + heal >= maxHealth)
            currentHealth = maxHealth;
        else currentHealth += heal;
    }

    public void CheckHealth() {
        if (currentHealth <= 0)
            OnDead();
    }

    private void OnDead() {
        throw new NotImplementedException();
    }
}
