using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : Enemy
{
    public GameObject projectylePrefab;
    public Transform aim;
    public float timeToHit;

    protected override void Initialize()
    {
        base.Initialize();
        AI.SetDefaultTarget(FindObjectOfType<Tower>().gameObject);
        AI.OnAttack += AI_OnAttack;
    }

    protected override void AI_OnAttack()
    {
        //StraightProyectile newBullet = Instantiate(projectylePrefab, aim.position, Quaternion.identity).GetComponent<StraightProyectile>();
        StraightProyectile newBullet = PoolManager.Instance.SpawnFromPool("fireball", aim.position, Quaternion.identity).GetComponent<StraightProyectile>();
        newBullet.Initialize(AI.CurrentTarget.transform.position, timeToHit, AI.targetMask);   
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
