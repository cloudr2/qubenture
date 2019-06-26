using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]

public class Tower : Entity, IUpdateable
{
    private HealthComponent HC = null;
    private GameObject currentTarget = null;
    public GameObject shootPrefab;
    public Transform aim;
    public float attackRate;
    public float attackRange;
    public LayerMask targetMask;
    public float timeToHit;

    private MeshRenderer MR;
    private List<GameObject> targetList = new List<GameObject>();
    private Material normal;
    public Material onHit;

    private float lastAttackTime = 0;

    public void OnEnable() {
        UpdateManager.Register(this);
    }

    public void OnDisable() {
        UpdateManager.Unregister(this);
    }

    void Start()
    {
        Initialize();
    }

    public void CustomUpdate()
    {
        Attack();
    }

    private void Initialize()
    {
        MR = GetComponentInChildren<MeshRenderer>();
        normal = MR.material;
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
            StraightProyectile newBullet = PoolManager.Instance.SpawnFromPool("fireball", aim.position, Quaternion.identity).GetComponent<StraightProyectile>();
            newBullet.Initialize(currentTarget.transform.position, timeToHit, targetMask);
        }
        
        Target();
    }

    private void TowerOnHit()
    {
        StartCoroutine(OnHitFX());
    }

    private IEnumerator OnHitFX() {
        MR.material = onHit;
        yield return new WaitForSeconds(3);
        MR.material = normal;
    }

    private void TowerOnDeath()
    {
        GameManager.Instance.EndGame("LOSE");
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

    public void CustomLateUpdate() {
        return;
    }
}
