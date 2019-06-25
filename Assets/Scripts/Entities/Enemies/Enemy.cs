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
            //FxManager.instance.PlayFx(FxManager.instance.enemyHitFx, transform.position);
            PoolManager.Instance.SpawnFromPool("enemyHitVFX", transform.position, Quaternion.identity);
        }
    }

    protected override void HealthComponent_OnDeath()
    {
        gameObject.SetActive(false);
        GameManager.instance.CheckEnemiesAlive();
    }
}
