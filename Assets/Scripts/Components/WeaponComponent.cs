using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    public WeaponBase myWeapon;

    public void Shoot(Vector3 direction) {
        GameObject newBullet = Instantiate(myWeapon.bulletPrefab,direction,Quaternion.identity);
    }
}
