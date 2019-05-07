using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LocomotionComponent))]

public class Player : Character
{
    private LocomotionComponent LC = null;
    public float rotationSpeed;
    public LayerMask targetMask;
    public Transform aim;
    public float meeleRange;
    public float meeleCooldown;
    public float rangedCooldown;
    public float damage;

    private float lastMeele;
    private float lastRanged;

    void Start()
    {
        Initialize();
    }

    void Update() {
        LC.Move(MoveDirection());
        FollowMouse();
        Attack();
    }

    private void FollowMouse()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit cameraRayHit;

        if (Physics.Raycast(cameraRay, out cameraRayHit))
            {
                Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                transform.LookAt(targetPosition);
            }
    }

    protected override void Initialize()
    {
        LC = GetComponent<LocomotionComponent>();
        base.Initialize();
    }

    private void Attack() {
        //attack animation
        if (Input.GetMouseButtonDown(0) && canUseMeele())
        {
            Collider[] targets = Physics.OverlapSphere(aim.position,meeleRange,targetMask);
            if (targets.Length > 0)
            {
                foreach (var target in targets)
                {
                    target.GetComponent<HealthComponent>().TakeDamage(damage);
                    print("attack!");
                }
            }
        }
    }

    private bool canUseMeele()
    {
        if (Time.time >= lastMeele + meeleCooldown)
        {
            lastMeele = Time.time;
            return true;
        }
        else
            return false;
    }

    private bool canUseRanged()
    {
        if (Time.time >= lastRanged + rangedCooldown)
        {
            lastRanged = Time.time;
            return true;
        }
        else
            return false;
    }

    protected override Vector3 MoveDirection() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        return new Vector3(horizontal, 0, vertical);
    }

    protected override void HealthComponent_OnDeath()
    {
        print("Player destroyed.");
        GameManager.instance.EndGame();
        //TODO: Dead animation
    }

    protected override void HealthComponent_OnHit()
    {
        if (canBeHit())
        {
            print("player hit: " + HC.CurrentHealth + " HP left.");
            //TODO: on hit animation
        }
    }

        private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(aim.position, meeleRange);
    }
}
