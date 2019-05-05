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
    public float attackRange;

    void Start()
    {
        Initialize();
    }

    void Update() {
        LC.Move(MoveDirection());
    }

    protected override void Initialize()
    {
        LC = GetComponent<LocomotionComponent>();
        base.Initialize();
    }

    private void Attack() {
        
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
        print("player hit: " + HC.CurrentHealth + " HP left.");
        //TODO: on hit animation
    }
}
