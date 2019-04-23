using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : ScriptableObject
{
    [Header("Weapon Stats")]
    public float fireRate;
    public float range;
    [Header("Weapon Bullet")]
    public GameObject bulletPrefab;
}
