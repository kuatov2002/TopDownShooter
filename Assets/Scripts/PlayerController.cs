using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;

    public Transform HandWithWeapon;
    public float rotateOffset;
    public Transform shotPoint;
    public GameObject projectile;

    public float shotCooldown;
    private float nextShotTime;

    private Vector2 movement;
    private Vector2 mousePosition;

    void Update()
    {
        // Получаем ввод передвижения игрока
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Получаем позицию мыши
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Поворот оружия
        Vector2 weaponToMouse = mousePosition - rb.position;
        float weaponAngle = Mathf.Atan2(weaponToMouse.y, weaponToMouse.x) * Mathf.Rad2Deg;
        HandWithWeapon.rotation = Quaternion.Euler(0f, 0f, weaponAngle + rotateOffset);

        // Стрельба
        if (Input.GetMouseButtonDown(0) && Time.time > nextShotTime)
        {
            nextShotTime = Time.time + shotCooldown;

            // Рассчитываем направление от точки выстрела к мыши
            Vector3 shotToMouse = mousePosition - (Vector2)shotPoint.position;
            float bulletAngle = Mathf.Atan2(shotToMouse.y, shotToMouse.x) * Mathf.Rad2Deg;

            // Создаем пулю с правильным поворотом
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, bulletAngle);
            Instantiate(projectile, shotPoint.position, bulletRotation);
        }
    }

    void FixedUpdate()
    {
        // Плавное передвижение игрока через Rigidbody
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}