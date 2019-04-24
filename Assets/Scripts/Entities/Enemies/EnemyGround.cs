using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGround : Enemy
{
    private float lastTime = 0;

    void Update()
    {
        LC.Move(MoveDirection());
        CheckTimer();
    }

    protected override void AssignNewTarget()
    {
        if (CurrentTarget == null)
            currentTarget = FindObjectOfType<Tower>().gameObject;
        else if (detectionDistance <= (CurrentTarget.transform.localPosition - transform.localPosition).magnitude)
            return;

        if (Random.Range(0, 1) >= 0.65f)
            currentTarget = FindObjectOfType<Player>().gameObject;

        print("current target is: " + CurrentTarget);
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
        if (currentTarget){
            Vector3 direction = CurrentTarget.transform.localPosition - transform.localPosition;
            return direction.normalized;
        }
        return transform.forward;
    }

    private void CheckTimer()
    {
        if(Time.time > lastTime + 3f)
        {
            AssignNewTarget();
            lastTime = Time.time;
        }
    }
}
