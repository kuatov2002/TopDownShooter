using UnityEngine;
using Zenject;

public class PlayerController : EntityBase
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float smoothTime = 0.1f; // Время сглаживания движения
    [SerializeField] private Joystick joystick; // Джойстик для управления

    [Header("Оружие")]
    [SerializeField] private Transform handWithWeapon; // Рука с оружием
    [SerializeField] private Transform shotPoint; // Точка выстрела
    [SerializeField] private GameObject projectile; // Префаб пули
    [SerializeField] private float shotCooldown = 0.5f; // Перезарядка стрельбы

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
        OnDeath += HandleDeath;
    }

    private void OnDestroy()
    {
        OnDeath -= HandleDeath;
    }

    private void HandleDeath()
    {
        Destroy(gameObject); // Удаляем объект при смерти
    }

    private void FixedUpdate()
    {
        HandleMovement();
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
