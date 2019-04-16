using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class Character : Entity
{
    [Header("Stats")]
    protected float health;
    protected float speed;

    protected float currentHealth;

    void Start() {
        currentHealth = health;
    }


}
