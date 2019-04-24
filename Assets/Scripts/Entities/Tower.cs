using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]

public class Tower : Entity
{
    private HealthComponent HC = null;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        HC = GetComponent<HealthComponent>();
        HC.OnHit += TowerOnHit;
        HC.OnDeath += TowerOnDeath;
    }

    private void TowerOnHit()
    {
        print("tower hit: " + HC.CurrentHealth + " HP left.");
        //TODO: On hit animation
    }

    private void TowerOnDeath()
    {
        print("Tower destroyed.");
        //TODO: Destroy animation
        GameManager.instance.EndGame();
    }
}
