using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EntityBase
{
    [SerializeField] private float detectionRange = 3f;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float attackDamage = 1f;
    [SerializeField] private float attackCooldown = 1f;

    private float lastAttackTime;

    Transform player;
    public int health;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<PlayerController>().transform;

        OnDeath += HandleDeath;
    }
    private void OnDestroy()
    {
        // Отписываемся от события смерти при уничтожении объекта
        OnDeath -= HandleDeath;
    }
    // Update is called once per frame
    void Update()
    {
        if (IsDead) return;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer <= attackRange)
            {
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    Attack();
                }
            }
            else
            {
                ChasePlayer();
            }
        }
    }
    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
    private void Attack()
    {
        lastAttackTime = Time.time;
        player.GetComponent<IHealth>()?.TakeDamage(attackDamage);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Projectile")
        {
            TakeDamage(other.GetComponent<Projectile>().damage);
        }
    }
    private void HandleDeath()
    {
        Destroy(gameObject);
    }
}
