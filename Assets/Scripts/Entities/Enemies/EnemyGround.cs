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
        anim.SetFloat("Speed", Mathf.Lerp(0, 1, AI.TargetDirectionNormalized.magnitude));
        return Vector3.zero;
    }

    protected override void AI_OnAttack() {
        AI.CurrentTarget.GetComponent<HealthComponent>().TakeDamage(AI.damage); ;
        anim.SetTrigger("Attack");
    }
}
