using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(LocomotionComponent))]
[RequireComponent(typeof(WeaponComponent))]
[RequireComponent(typeof(Animator))]

public abstract class Character : Entity
{
    protected HealthComponent HC = null;
    protected LocomotionComponent LC = null;
    protected WeaponComponent WC = null;
    protected Animator anim = null;

    void Start() {
        Initialize();
    }

    private void Initialize() {
        HC = GetComponent<HealthComponent>();
        LC = GetComponent<LocomotionComponent>();
        WC = GetComponent<WeaponComponent>();
        anim = GetComponent<Animator>();

        HC.OnHit += HealthComponent_OnHit;
        HC.OnDeath += HealthComponent_OnDeath;
    }

    protected abstract void HealthComponent_OnHit();
    protected abstract void HealthComponent_OnDeath();
    protected abstract Vector3 MoveDirection();
}
