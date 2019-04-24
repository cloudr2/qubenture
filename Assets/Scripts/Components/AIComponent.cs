using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class AIComponent : MonoBehaviour {
    
    private float lastTime = 0;
    private Vector3 targetDirection;
    private GameObject currentTarget = null;
    private List<GameObject> targetList = new List<GameObject>();
    private enum States {SEARCH, FOLLOW, ATTACK}
    private States currentState;

    public float speed;
    public float awareness;
    public float attackRange;
    public float attackRate;
    public GameObject defaultTarget;
    public LayerMask targetMask;

    public Vector3 TargetDirectionNormalized { get { return targetDirection.normalized; } }
    public GameObject CurrentTarget { get { return currentTarget; } }


    private void Start() {
        Initialize();
    }

    private void Update() {
        VageFSM();
    }

    private void Initialize() {
        currentState = States.SEARCH;
    }

    private void VageFSM() {
        if (currentState == States.SEARCH)
            Search();
        else if (currentState == States.FOLLOW)
            Follow();
        else if (currentState == States.ATTACK)
            Attack();
        else
            Search();

        print(currentState);
    }


    private void Search() {
        transform.localPosition += transform.forward * speed * Time.deltaTime;
        currentTarget = null;
        Collider[] hits = Physics.OverlapSphere(transform.localPosition, awareness, targetMask);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                targetList.Insert(i, hits[i].gameObject);
            }
            currentTarget = targetList[0];
            currentState = States.FOLLOW;
        }
     }

    private void Follow()
    {
        LookAtTarget();
        transform.localPosition += TargetDirectionNormalized * speed * Time.deltaTime;
        if (targetDirection.magnitude <= attackRange)
            currentState = States.ATTACK;
        else if (targetDirection.magnitude > awareness)
            currentState = States.SEARCH;
        else
            return;
    }

    private void Attack()
    {
        if (CanAttack() && currentTarget.GetComponent<HealthComponent>())
        {
            currentTarget.GetComponent<HealthComponent>().TakeDamage(10); ;
            print("Attakc!");
        }

        LookAtTarget();
        if (targetDirection.magnitude > attackRange)
            currentState = States.FOLLOW;
        else if (targetDirection.magnitude > awareness)
            currentState = States.SEARCH;
        else
            return;
    }

    private void LookAtTarget()
    {
        if (CurrentTarget != null)
        {
            targetDirection = currentTarget.transform.localPosition - transform.localPosition;
            transform.forward = TargetDirectionNormalized;
        }
    }

    private bool CanAttack()
    {
        if (Time.time >= lastTime + attackRate)
        {
            lastTime = Time.time;
            return true;
        }
        else
            return false;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.localPosition, awareness);
    }
}
