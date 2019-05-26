using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGround : Enemy
{
    protected override void Initialize()
    {
        base.Initialize();
        AI.SetDefaultTarget(FindObjectOfType<Player>().gameObject);
        AI.OnAttack += AI_OnAttack;
    }

    protected override void HealthComponent_OnHit()
    {
        base.HealthComponent_OnHit();
        anim.SetTrigger("OnHit");
    }

    protected override void HealthComponent_OnDeath()
    {
        base.HealthComponent_OnDeath();
    }

    protected override Vector3 MoveDirection()
    {
        return Vector3.zero;
    }

    protected override void AI_OnAttack() {
        anim.SetTrigger("Attack");
        AI.CurrentTarget.GetComponent<HealthComponent>().TakeDamage(AI.damage); ;
    }
}
