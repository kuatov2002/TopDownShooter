using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Настройки камеры")]
    public Transform target;            // Цель, за которой будет следовать камера (игрок)
    public float smoothSpeed = 0.125f;  // Скорость плавного следования
    public Vector3 offset;              // Смещение камеры от игрока

    void LateUpdate()
    {
        if (target == null) return;

        // Позиция, куда должна переместиться камера
        Vector3 desiredPosition = target.position + offset;

        // Плавное движение камеры к нужной позиции
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Устанавливаем позицию камеры
        transform.position = smoothedPosition;
    }
}