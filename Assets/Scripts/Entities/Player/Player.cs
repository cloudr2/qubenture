using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IHiteable, IDestructible
{
    

    public void Destroy() {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            Destroy();
        }
    }
}
