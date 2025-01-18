using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : EntityBase
{
    [SerializeField] private float detectionRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackCooldown ;
    
    private float lastAttackTime;

    Transform player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<PlayerController>().transform;

        OnHealthChanged += HPChanged;
        OnDeath += HandleDeath;
    }
    private void OnDestroy()
    {
        OnHealthChanged -= HPChanged;
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
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Projectile")
        {
            Destroy(collider.gameObject);
            TakeDamage(collider.GetComponent<Projectile>().damage);
        }
    }

    private void HPChanged()
    {
        hpBar.value = currentHealth;
    }
    private void HandleDeath()
    {
        Destroy(gameObject);
    }
}