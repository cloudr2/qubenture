using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIComponent))]

public abstract class Enemy : Character
{
    protected AIComponent AI = null;

    void Start()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        AI = GetComponent<AIComponent>();
        base.Initialize();
    }

    protected abstract void AI_OnAttack();

    protected override void HealthComponent_OnHit()
    {

        if (canBeHit())
        {
            print("Enemy hit: " + HC.CurrentHealth + " HP left.");
            //TODO: Play on hit animation;
        }
    }

    protected override void HealthComponent_OnDeath()
    {
        //TODO: Play on Death animation;
        gameObject.SetActive(false);
    }
}
