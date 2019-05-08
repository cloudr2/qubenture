using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]

public class Tower : Entity
{
    private HealthComponent HC = null;
    private GameObject currentTarget = null;
    public GameObject shootPrefab;
    public Transform aim;
    public float attackRate;
    public float attackRange;
    public LayerMask targetMask;
    public float timeToHit;

    private List<GameObject> targetList = new List<GameObject>();

    private float lastAttackTime = 0;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        Attack();
    }

    private void Initialize()
    {
        HC = GetComponent<HealthComponent>();
        HC.OnHit += TowerOnHit;
        HC.OnDeath += TowerOnDeath;
    }

    private void Target()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, attackRange / 2, targetMask);
        if (targets.Length > 0)
        {
            if (targets[0].GetComponent<HealthComponent>())
            {
                targetList.Insert(0, targets[0].gameObject);
                currentTarget = targetList[0];
            }
        }
        else
            currentTarget = null;
    }

    private void Attack()
    {
        if (CanAttack())
        {
            StraightProyectile newBullet = Instantiate(shootPrefab, aim.position, Quaternion.identity).GetComponent<StraightProyectile>();
            newBullet.Initialize(currentTarget.transform.position, timeToHit, targetMask);
        }
        
        Target();
    }

    private void TowerOnHit()
    {
        print("tower hit: " + HC.CurrentHealth + " HP left.");
        //TODO: On hit animation
    }

    private void TowerOnDeath()
    {
        print("Tower destroyed.");
        //TODO: Destroy animation
        GameManager.instance.EndGame("Lose");
    }

    private bool CanAttack()
    {
        if (currentTarget == null)
        {
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
