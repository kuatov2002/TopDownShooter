using UnityEngine;

public class PlayerController : EntityBase
{
    public float moveSpeed = 5f;
    public float smoothTime = 0.1f; // Время сглаживания движения
    public Rigidbody2D rb;

    [Header("Оружие")]
    public Transform HandWithWeapon;
    public Transform shotPoint;
    public GameObject projectile;

    [Header("Стрельба")]
    public float shotCooldown;
    private float nextShotTime;

    [Header("Джойстик")]
    public Joystick joystick;

    private Vector2 movement;
    private Vector2 currentVelocity; // Для сглаживания движения


    protected override void Start()
    {
        base.Start();
        OnDeath += HandleDeath;
    }
    private void OnDestroy()
    {
        // Отписываемся от события смерти при уничтожении объекта
        OnDeath -= HandleDeath;
    }
    private void HandleDeath()
    {
        Destroy(gameObject);
    }
    void Update()
    {
    }
    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Получаем ввод с джойстика
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        // Если персонаж движется, поворачиваем его в сторону движения
        if (movement != Vector2.zero)
        {
            float weaponAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            HandWithWeapon.rotation = Quaternion.Euler(0f, 0f, weaponAngle);

        }
        // Плавное движение персонажа через Rigidbody
        Vector2 targetVelocity = movement * moveSpeed;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, smoothTime);
    }

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + shotCooldown;

            // Создаем пулю в направлении оружия
            Instantiate(projectile, shotPoint.position, HandWithWeapon.rotation);
        }
    }
}