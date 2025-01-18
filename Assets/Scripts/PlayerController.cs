using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PlayerController : EntityBase
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float smoothTime = 0.1f; // Время сглаживания движения
    [SerializeField] private Joystick joystick; // Джойстик для управления

    [Header("Оружие")]
    [SerializeField] private float autoAimRange = 4f;
    [SerializeField] private Transform handWithWeapon; // Рука с оружием
    [SerializeField] private Transform shotPoint; // Точка выстрела
    [SerializeField] private GameObject projectile; // Префаб пули
    [SerializeField] private float shotCooldown = 0.5f; // Перезарядка стрельбы
    private Transform nearestEnemy;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 currentVelocity; // Для сглаживания движения
    private float nextShotTime;

    [Inject]
    private void Construct(IInventoryManager inventoryManager, ILootCollector lootCollector)
    {
        lootCollector.Initialize(inventoryManager);
    }


    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        OnHealthChanged += HPChanged;
        OnDeath += HandleDeath;
    }

    private void OnDestroy()
    {
        OnHealthChanged -= HPChanged;
        OnDeath -= HandleDeath;
    }

    private void HandleDeath()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void HPChanged()
    {
        hpBar.value = currentHealth;
    }
    private void FixedUpdate()
    {
        HandleMovement();
        HandleAutoAim();
    }

    private void HandleMovement()
    {
        // Получаем ввод с джойстика
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        // Поворачиваем оружие в направлении движения
        if (movement != Vector2.zero)
        {
            float weaponAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            handWithWeapon.rotation = Quaternion.Euler(0f, 0f, weaponAngle);
        }

        // Плавное движение через Rigidbody
        Vector2 targetVelocity = movement * moveSpeed;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, smoothTime);
    }
    private void HandleAutoAim()
    {
        // Ищем всех врагов в радиусе
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, autoAimRange);
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy")) // Проверяем, что объект - враг
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestEnemy = hit.transform;
                    closestDistance = distance;
                }
            }
        }

        nearestEnemy = closestEnemy;

        // Если враг найден, направляем оружие на него
        if (nearestEnemy != null)
        {
            Vector2 directionToEnemy = (nearestEnemy.position - handWithWeapon.position).normalized;
            float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;
            handWithWeapon.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + shotCooldown;

            // Создаем пулю в направлении оружия
            Instantiate(projectile, shotPoint.position, handWithWeapon.rotation);
        }
    }


}
