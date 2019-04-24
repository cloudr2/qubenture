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

    protected override void HealthComponent_OnHit()
    {
        //TODO: Play on hit animation;
    }

    protected override void HealthComponent_OnDeath()
    {
        //TODO: Play on Death animation;
    }

}
