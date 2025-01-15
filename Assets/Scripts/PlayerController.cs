using UnityEngine;

public class PlayerController : EntityBase
{
    public float moveSpeed = 5f;
    public float smoothTime = 0.1f; // ����� ����������� ��������
    public Rigidbody2D rb;

    [Header("������")]
    public Transform HandWithWeapon;
    public Transform shotPoint;
    public GameObject projectile;

    [Header("��������")]
    public float shotCooldown;
    private float nextShotTime;

    [Header("��������")]
    public Joystick joystick;

    private Vector2 movement;
    private Vector2 currentVelocity; // ��� ����������� ��������


    protected override void Start()
    {
        base.Start();
        OnDeath += HandleDeath;
    }
    private void OnDestroy()
    {
        // ������������ �� ������� ������ ��� ����������� �������
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
        // �������� ���� � ���������
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        // ���� �������� ��������, ������������ ��� � ������� ��������
        if (movement != Vector2.zero)
        {
            float weaponAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            HandWithWeapon.rotation = Quaternion.Euler(0f, 0f, weaponAngle);
        }
        // ������� �������� ��������� ����� Rigidbody
        Vector2 targetVelocity = movement * moveSpeed;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, smoothTime);
    }

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + shotCooldown;

            // ������� ���� � ����������� ������
            Instantiate(projectile, shotPoint.position, HandWithWeapon.rotation);
        }
    }
}