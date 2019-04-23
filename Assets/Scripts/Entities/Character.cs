using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(LocomotionComponent))]
[RequireComponent(typeof(WeaponComponent))]

public abstract class Character : Entity
{
    protected HealthComponent HC;
    protected LocomotionComponent LC;
    protected WeaponComponent WC;

    void Start() {
        HC = GetComponent<HealthComponent>();
        LC = GetComponent<LocomotionComponent>();
        WC = GetComponent<WeaponComponent>();
    }
}
