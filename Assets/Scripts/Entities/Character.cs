using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(WeaponComponent))]
[RequireComponent(typeof(Animator))]

public abstract class Character : Entity
{
    protected HealthComponent HC = null;
    protected WeaponComponent WC = null;
    protected Animator anim = null;

    void Start() {
        Initialize();
    }

    protected virtual void Initialize() {
        HC = GetComponent<HealthComponent>();
        WC = GetComponent<WeaponComponent>();
        anim = GetComponent<Animator>();

        HC.OnHit += HealthComponent_OnHit;
        HC.OnDeath += HealthComponent_OnDeath;
    }

    protected abstract void HealthComponent_OnHit();
    protected abstract void HealthComponent_OnDeath();
    protected abstract Vector3 MoveDirection();

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.localPosition, transform.localPosition + transform.forward * 1.5f);
    }
}
