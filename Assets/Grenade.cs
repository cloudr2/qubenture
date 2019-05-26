using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {
    public float detectionRadius;
    public LayerMask targetMask {get; set;}
    public float damage;
    public GameObject explosionFX;

    void Update() {
        Detonate();
    }

    private void Detonate() {
        Collider[] targets = Physics.OverlapSphere(transform.position, detectionRadius, targetMask);
        if (targets.Length > 0) {
            foreach (var target in targets) {
                if(target.GetComponent<HealthComponent>())
                    target.GetComponent<HealthComponent>().TakeDamage(damage);
            }
            Instantiate(explosionFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,detectionRadius);
    }
}
