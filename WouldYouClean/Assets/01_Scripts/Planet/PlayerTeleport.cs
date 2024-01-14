using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] Transform _map;

    private void Update()
    {
        // ���� ��ġ�� ũ�� ����
        float objectPosX = transform.position.x;
        float objectPosY = transform.position.y;
        float objectScaleX = transform.localScale.x;
        float objectScaleY = transform.localScale.y;

        // ���� ��ġ�� ũ�� ����
        float mapPosX = _map.position.x;
        float mapPosY = _map.position.y;
        float mapScaleX = _map.localScale.x;
        float mapScaleY = _map.localScale.y;

        // �����¿� ��ġ ���
        float rightBoundary = mapPosX + (mapScaleX / 2);
        float leftBoundary = -mapPosX - (mapScaleX / 2);
        float topBoundary = mapPosY + (mapScaleY / 2);
        float bottomBoundary = -mapPosY - (mapScaleY / 2);

        // ������, ����, ����, �Ʒ������� �������� Ȯ��
        bool isOutsideRight = objectPosX + (objectScaleX / 2) > rightBoundary;
        bool isOutsideLeft = objectPosX - (objectScaleX / 2) < leftBoundary;
        bool isOutsideTop = objectPosY + (objectScaleY / 2) > topBoundary;
        bool isOutsideBottom = objectPosY - (objectScaleY / 2) < bottomBoundary;

        // ��� ��� �� �߾����� �̵�
        if (isOutsideRight)
        {
            transform.position = new Vector3(leftBoundary + objectScaleX / 2, objectPosY, transform.position.z);
            print("���������� ����");
        }
        if (isOutsideLeft)
        {
            transform.position = new Vector3(rightBoundary - objectScaleX / 2, objectPosY, transform.position.z);
            print("�������� ����");
        }
        if (isOutsideTop)
        {
            transform.position = new Vector3(objectPosX, bottomBoundary + objectScaleY / 2, transform.position.z);
            print("�������� ����");
        }
        if (isOutsideBottom)
        {
            transform.position = new Vector3(objectPosX, topBoundary - objectScaleY / 2, transform.position.z);
            print("�Ʒ����� ����");
        }
    }
}
