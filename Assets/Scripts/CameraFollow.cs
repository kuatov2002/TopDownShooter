using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 3f;          // ����������� �������� ��� ����� ��������� �����������
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero; // ���������� ��� �������� ������� �������� ��������

    private void LateUpdate()
    {
        if (target == null) return;

        // ������������ �������� ������� ������
        Vector3 desiredPosition = target.position + offset;

        // ���������� SmoothDamp ������ Lerp ��� ����� �������� ��������
        // SmoothDamp ������� ������ �������, ��� ���� ����� ������������ ����������
        transform.position = Vector3.SmoothDamp(
            transform.position,    // ������� �������
            desiredPosition,       // ������� �������
            ref velocity,          // ������� �������� �������� (����������� �������������)
            1f / smoothSpeed,      // ����� �� ���������� ���� (������ = �������)
            float.PositiveInfinity,// ������������ �������� (��� �����������)
            Time.deltaTime         // ��������� ����� ����� �������
        );
    }
}