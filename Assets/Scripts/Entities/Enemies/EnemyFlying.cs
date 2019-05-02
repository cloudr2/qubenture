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

    private void AI_OnAttack()
    {
        Vector3 Temp = AI.CurrentTarget.transform.position;
        Temp.y -= 1;
       StraightProyectile newBullet = Instantiate(projectylePrefab, Temp, Quaternion.identity).GetComponent<StraightProyectile>();
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
