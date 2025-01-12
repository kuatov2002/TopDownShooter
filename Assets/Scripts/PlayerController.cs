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
        // �������� ���� ������������ ������
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // �������� ������� ����
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // ������� ������
        Vector2 weaponToMouse = mousePosition - rb.position;
        float weaponAngle = Mathf.Atan2(weaponToMouse.y, weaponToMouse.x) * Mathf.Rad2Deg;
        HandWithWeapon.rotation = Quaternion.Euler(0f, 0f, weaponAngle + rotateOffset);

        // ��������
        if (Input.GetMouseButtonDown(0) && Time.time > nextShotTime)
        {
            nextShotTime = Time.time + shotCooldown;

            // ������������ ����������� �� ����� �������� � ����
            Vector3 shotToMouse = mousePosition - (Vector2)shotPoint.position;
            float bulletAngle = Mathf.Atan2(shotToMouse.y, shotToMouse.x) * Mathf.Rad2Deg;

            // ������� ���� � ���������� ���������
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, bulletAngle);
            Instantiate(projectile, shotPoint.position, bulletRotation);
        }
    }

    void FixedUpdate()
    {
        // ������� ������������ ������ ����� Rigidbody
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}