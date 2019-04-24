using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class AIComponent : MonoBehaviour {
    
    private float lastTime = 0;
    private GameObject currentTarget = null;

    private enum States {SEARCH, FOLLOW, ATTACK}
    private States currentState;
    private SphereCollider detectionCollider;

    public float awareness;
    public Vector3 targetDirection;
    public GameObject CurrentTarget { get { return currentTarget; } }

    private void Start() {
        Initialize();
    }

    private void Update() {
        VageFSM();
    }

    private void Initialize() {
        detectionCollider = GetComponent<SphereCollider>();
        detectionCollider.radius = awareness;
        detectionCollider.isTrigger = true;
        detectionCollider.transform.position = transform.localPosition;
        currentState = States.SEARCH;
    }

    private void VageFSM() {
        if(currentState == States.SEARCH || currentTarget == null) {
            SearchNewTarget();
        } else if (currentState == States.FOLLOW && currentTarget != null) {
            
        }
    }

    private void SearchNewTarget() {
        currentTarget = defaultTarget;
        currentState = States.FOLLOW;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.localPosition, awareness);
    }
}
