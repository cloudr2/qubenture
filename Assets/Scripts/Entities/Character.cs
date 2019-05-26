using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(HealthComponent))]

public abstract class Character : Entity
{
    protected HealthComponent HC = null;
    protected Animator anim = null;
    [SerializeField]
    protected float hitCD = 1f;
    protected float lastHit;

    void Start() {
        Initialize();
    }

    protected virtual void Initialize() {
        HC = GetComponent<HealthComponent>();
        anim = GetComponentInChildren<Animator>();

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

    protected bool canBeHit()
    {
        if (Time.time >= lastHit + hitCD)
        {
            lastHit = Time.time;
            return true;
        }
        else
            return false;
    }
}
