﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public GameObject projectylePrefab;
    public Transform aim;
    public float timeToHit;

    protected override void Initialize() {
        base.Initialize();
        AI.SetDefaultTarget(FindObjectOfType<Player>().gameObject);
        AI.OnAttack += AI_OnAttack;
    }

    protected override void AI_OnAttack() {
        StraightProyectile newBullet = Instantiate(projectylePrefab, aim.position, Quaternion.identity).GetComponent<StraightProyectile>();
        newBullet.Initialize(AI.CurrentTarget.transform.position, 0.2f, AI.targetMask);
    }

    protected override void HealthComponent_OnHit() {
        base.HealthComponent_OnHit();
    }

    protected override void HealthComponent_OnDeath() {
        base.HealthComponent_OnDeath();
    }

    protected override Vector3 MoveDirection() {
        return Vector3.zero;
    }
}
