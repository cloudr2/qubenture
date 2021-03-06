﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LocomotionComponent))]

public class Player : Character, IUpdateable
{
    [Header("Stats")]
    public float damage;
    public float speed;
    public float meeleRange;
    public float meeleCooldown;
    public float rangedCooldown;
    public float rangedDuration;

    [Header("Shooting Point")]
    public Transform aim;
    public Transform startPoint;
    public Transform bezierPoint;
    public Transform destinationPoint;
    public LayerMask targetMask;
    public Slider hpBar;
    public GameObject grenadePrefab;

    private float lastMeele;
    private float lastRanged;
    private bool rangedAnimationFinished = true;
    private VirtualJoystick VJ;
    private Vector3 forward, right;

    public void OnEnable() {
        UpdateManager.Register(this);
    }

    public void OnDisable() {
        UpdateManager.Unregister(this);
    }

    void Awake() {
        VJ = FindObjectOfType<VirtualJoystick>();
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    public void CustomUpdate() {
        Move(MoveDirection());
        Attack();
        ThrowGrenade();
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

    private void ThrowGrenade() {
        if (UIManager.instance.grenadeButton.IsPressed && canUseGrenade()) {
            GameObject newGrenade = Instantiate(grenadePrefab, aim.position,Quaternion.identity).gameObject;
            newGrenade.GetComponent<Grenade>().targetMask = this.targetMask;
            if (newGrenade)
                StartCoroutine(GrenadeBezier(newGrenade));
        }
    }

    private IEnumerator GrenadeBezier(GameObject go) {
        rangedAnimationFinished = false;
        float startTime = Time.time;
        float percent = 0;
        Vector3 p0 = aim.position;
        Vector3 p1 = bezierPoint.position;
        Vector3 p2 = destinationPoint.position;

        while (percent <= 1 || !go) {
            percent = (Time.time - startTime) / rangedDuration;
            Vector3 A = Vector3.Lerp(p0, p1, percent);
            Vector3 B = Vector3.Lerp(p1, p2, percent);
            if(go)
                go.transform.position = Vector3.Lerp(A, B, percent);

            yield return null;
        }
        rangedAnimationFinished = true;
        Destroy(go);
    }

    private bool canUseGrenade() {
        if (Time.time >= lastRanged + rangedCooldown) {
            lastRanged = Time.time;
            return true;
        }
        else
            return false;
    }

    private void Attack() {
        if (UIManager.instance.attackButton.IsPressed && canUseMeele())
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
        if (Time.time >= lastRanged + rangedCooldown && rangedAnimationFinished)
        {
            lastRanged = Time.time;
            return true;
        }
        else
            return false;
    }

    protected override Vector3 MoveDirection() {
        float horizontal = VJ.VJHorizontalAxis();
        float vertical = VJ.VJVerticalAxis();
        Vector3 rightMovement = right * horizontal;
        Vector3 upMovement = forward * vertical;
        Vector3 towardsDir = Vector3.Normalize(rightMovement + upMovement);

        if (towardsDir != Vector3.zero) {
            transform.forward = towardsDir;
        }
        return towardsDir;
    }

    protected override void HealthComponent_OnDeath()
    {
        print("Player destroyed.");
        GameManager.Instance.EndGame("LOSE");
    }

    protected override void HealthComponent_OnHit()
    {
        hpBar.value = (HC.CurrentHealth / HC.MaxHealth);
        if (canBeHit())
        {
            //FxManager.instance.PlayFx(FxManager.instance.playerHitFx,transform.position);
            PoolManager.Instance.SpawnFromPool("playerHitVFX", transform.position, Quaternion.identity);

            anim.SetTrigger("OnHit");
        }
    }

        private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(aim.position, meeleRange);
    }

    public void CustomLateUpdate() {
        return;
    }
}
