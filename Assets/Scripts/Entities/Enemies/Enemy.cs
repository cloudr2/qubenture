using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{
    public GameObject CurrentTarget { get { return currentTarget; } }

    [SerializeField]
    protected float detectionDistance;
    protected GameObject currentTarget = null;

    protected abstract void AssignNewTarget();

    protected override void HealthComponent_OnHit()
    {
        //TODO: Play on hit animation;
    }

    protected override void HealthComponent_OnDeath()
    {
        //TODO: Play on Death animation;
    }

}
