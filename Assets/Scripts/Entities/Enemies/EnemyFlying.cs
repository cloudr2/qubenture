using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : Enemy
{
    public GameObject projectylePrefab;
    public Transform aim;

    protected override void Initialize()
    {
        base.Initialize();
        AI.SetDefaultTarget(FindObjectOfType<Tower>().gameObject);
        AI.OnAttack += AI_OnAttack;
    }

    protected override void AI_OnAttack()
    {
       StraightProyectile newBullet = Instantiate(projectylePrefab, aim.position, Quaternion.identity).GetComponent<StraightProyectile>();
       newBullet.Initialize(AI.CurrentTarget.transform.position, AI.attackRate, AI.targetMask);   
    }

    protected void AI_OnAttack2()
    {

        // play on attack animtaion
    }
    
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
