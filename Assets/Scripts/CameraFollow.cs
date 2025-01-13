using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 3f;          // Увеличенное значение для более заметного сглаживания
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero; // Переменная для хранения текущей скорости движения

    private void LateUpdate()
    {
        if (target == null) return;

        // Рассчитываем желаемую позицию камеры
        Vector3 desiredPosition = target.position + offset;

        // Используем SmoothDamp вместо Lerp для более плавного движения
        // SmoothDamp создает эффект пружины, что дает более естественное следование
        transform.position = Vector3.SmoothDamp(
            transform.position,    // Текущая позиция
            desiredPosition,       // Целевая позиция
            ref velocity,          // Текущая скорость движения (обновляется автоматически)
            1f / smoothSpeed,      // Время до достижения цели (меньше = быстрее)
            float.PositiveInfinity,// Максимальная скорость (без ограничения)
            Time.deltaTime         // Учитываем время между кадрами
        );
    }
}