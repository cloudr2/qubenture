using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider))]

public class AIComponent : MonoBehaviour {
    
    private float lastAttackTime = 0;
    private float lastTargetChange = 0;
    private Vector3 targetDirection;
    private GameObject defaultTarget = null;
    private GameObject currentTarget = null;
    private List<GameObject> targetList = new List<GameObject>();
    private enum States {SEARCH, FOLLOW, ATTACK}
    private States currentState;

    public float speed;
    public float awareness;
    public float attackRange;
    public float attackRate;
    public float damage;
    public LayerMask targetMask;

    public Vector3 TargetDirectionNormalized { get { return targetDirection.normalized; } }
    public GameObject CurrentTarget { get { return currentTarget; } }

    public event System.Action OnAttack = delegate () { };

    private void Start() {
        Initialize();
    }

    private void Update() {
        VageFSM();
        LookAtTarget();
    }

    private void Initialize() {
        currentState = States.SEARCH;
    }

    public void SetDefaultTarget(GameObject target)
    {
        defaultTarget = target;
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
    }

    private void Search()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, awareness, targetMask);
        
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                targetList.Insert(i, hits[i].gameObject);
            }
            currentTarget = targetList[0];
        }
        else
        {
            currentTarget = defaultTarget;
        }
            currentState = States.FOLLOW;
    }

    private void CheckChangeTarget()
    {
        if(Random.Range(0,10) >= 8 && CanChangeTarget()) {
            if (currentTarget != defaultTarget)
            {
                currentTarget = defaultTarget;
                Search();
            }
        }
    }

    private void Follow()
    {
        transform.position += TargetDirectionNormalized * speed * Time.deltaTime;
        if (targetDirection.magnitude <= attackRange)
            currentState = States.ATTACK;
        else if (targetDirection.magnitude > awareness)
            currentState = States.SEARCH;
        else
            return;
    }

    private void Attack()
    {
        if (CanAttack())
        {
            OnAttack();
            CheckChangeTarget();
        }

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
            targetDirection = currentTarget.transform.position - transform.position;
            targetDirection.y = 0;
            transform.forward = TargetDirectionNormalized;
        }
    }

    private bool CanAttack()
    {
        if (currentTarget == null) {
            return false;
        }
        if (Time.time >= lastAttackTime + attackRate && currentTarget.GetComponent<HealthComponent>())
        {
            lastAttackTime = Time.time;
            return true;
        }
        else
            return false;
    }

    private bool CanChangeTarget()
    {
        if (Time.time >= lastTargetChange + 1)
        {
            lastTargetChange = Time.time;
            return true;
        }
        else
            return false;
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, awareness);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
