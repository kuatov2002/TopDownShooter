using UnityEngine;
using Zenject;

public class PlayerController : EntityBase
{
    [SerializeField] private float moveSpeed = 5f;
    private float smoothTime = 0.1f; // Время сглаживания движения
    public Rigidbody2D rb;

    [Header("Взаимодействие")]
    public LayerMask lootLayer; // Установите это в инспекторе на слой "Loot"
    public float interactionRadius; // Выносим радиус в настраиваемое поле

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

    private IInventoryManager _inventoryManager;

    [Inject]
    public void Construct(IInventoryManager inventoryManager)
    {
        _inventoryManager = inventoryManager;
    }

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

    void FixedUpdate()
    {
        HandleMovement();
    }

    public void Interact()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(
            transform.position,
            interactionRadius,
            lootLayer
        );

        foreach (var hitCollider in hitColliders)
        {
            IInteractable interactable = hitCollider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                break;
            }
        }
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

    private void OnDrawGizmos()
    {
        // Рисуем сферу взаимодействия желтым цветом
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}