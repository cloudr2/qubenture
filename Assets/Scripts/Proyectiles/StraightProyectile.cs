using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProyectile : MonoBehaviour, IUpdateable {
    float velocity;
    Vector3 direction;
    LayerMask collisionLayerMask;

    public float damage = 10;
    public float radius = 0.5f;

    public void OnEnable() {
        UpdateManager.Register(this);
    }

    public void OnDisable() {
        UpdateManager.Unregister(this);
    }

    public void Initialize(Vector3 destination, float time, LayerMask collisionLayerMask) {
        velocity = Vector3.Distance(transform.position, destination) / time;
        direction = destination - transform.position;
        direction.Normalize();
        this.collisionLayerMask = collisionLayerMask;
        //Destroy(this.gameObject,2f);
    }

    public void CustomUpdate() {
        transform.localPosition += direction * velocity * Time.deltaTime;
        CheckCollision();
    }

    void CheckCollision() {
        Collider[] hits = Physics.OverlapSphere(transform.localPosition, radius, collisionLayerMask);
        for(int i = 0; i < hits.Length; i++) 
        {
            Collider hit = hits[i];
            HealthComponent healthComponent = hit.GetComponent<HealthComponent>();
            if (healthComponent == null)
                continue;

            DoDamage(healthComponent);
            //Destroy(this.gameObject);
            gameObject.SetActive(false);
        }
    }

    void DoDamage(HealthComponent targetHit) {
        targetHit.TakeDamage(damage);
    }

    public void CustomLateUpdate() {
        return;
    }
}
