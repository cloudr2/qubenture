using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LocomotionComponent))]

public class Player : Character
{
    public LayerMask targetMask;
    public Transform aim;
    public float meeleRange;
    public float meeleCooldown;
    public float rangedCooldown;
    public float damage;
    public float speed;
    public Slider hpBar;

    private float lastMeele;
    private float lastRanged;


    void Update() {
        Move(MoveDirection());
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

    public void Move(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
        anim.SetFloat("Speed",Mathf.Lerp(0,1,MoveDirection().magnitude));
    }

    private void Attack() {
        if (Input.GetMouseButtonDown(0) && canUseMeele())
        {
            anim.SetTrigger("Attack");
            Collider[] targets = Physics.OverlapSphere(aim.position,meeleRange,targetMask);
            if (targets.Length > 0)
            {
                foreach (var target in targets)
                {
                    target.GetComponent<HealthComponent>().TakeDamage(damage);
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
        GameManager.instance.EndGame("Lose");
        //TODO: Dead animation
    }

    protected override void HealthComponent_OnHit()
    {
        if (canBeHit())
        {
            FxManager.instance.PlayFx(FxManager.instance.playerHitFx,transform.position);
            hpBar.value = (HC.CurrentHealth / HC.MaxHealth);
            anim.SetTrigger("OnHit");
        }
    }

        private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(aim.position, meeleRange);
    }
}
