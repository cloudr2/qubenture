﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : Enemy
{
    protected override void HealthComponent_OnHit()
    {
        base.HealthComponent_OnHit();
    }

    protected override void HealthComponent_OnDeath()
    {
        base.HealthComponent_OnDeath();
    }

    protected override Vector3 MoveDirection()
    {
        return Vector3.zero;
    }
}
