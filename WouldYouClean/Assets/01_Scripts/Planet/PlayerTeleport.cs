using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] Transform _map;

    private void Update()
    {
        // 현재 위치와 크기 변수
        float objectPosX = transform.position.x;
        float objectPosY = transform.position.y;
        float objectScaleX = transform.localScale.x;
        float objectScaleY = transform.localScale.y;

        // 맵의 위치와 크기 변수
        float mapPosX = _map.position.x;
        float mapPosY = _map.position.y;
        float mapScaleX = _map.localScale.x;
        float mapScaleY = _map.localScale.y;

        // 상하좌우 위치 계산
        float rightBoundary = mapPosX + (mapScaleX / 2);
        float leftBoundary = -mapPosX - (mapScaleX / 2);
        float topBoundary = mapPosY + (mapScaleY / 2);
        float bottomBoundary = -mapPosY - (mapScaleY / 2);

        // 오른쪽, 왼쪽, 위쪽, 아래쪽으로 나가는지 확인
        bool isOutsideRight = objectPosX + (objectScaleX / 2) > rightBoundary;
        bool isOutsideLeft = objectPosX - (objectScaleX / 2) < leftBoundary;
        bool isOutsideTop = objectPosY + (objectScaleY / 2) > topBoundary;
        bool isOutsideBottom = objectPosY - (objectScaleY / 2) < bottomBoundary;

        // 결과 출력 및 중앙으로 이동
        if (isOutsideRight)
        {
            transform.position = new Vector3(leftBoundary + objectScaleX / 2, objectPosY, transform.position.z);
            print("오른쪽으로 나감");
        }
        if (isOutsideLeft)
        {
            transform.position = new Vector3(rightBoundary - objectScaleX / 2, objectPosY, transform.position.z);
            print("왼쪽으로 나감");
        }
        if (isOutsideTop)
        {
            transform.position = new Vector3(objectPosX, bottomBoundary + objectScaleY / 2, transform.position.z);
            print("위쪽으로 나감");
        }
        if (isOutsideBottom)
        {
            transform.position = new Vector3(objectPosX, topBoundary - objectScaleY / 2, transform.position.z);
            print("아래으로 나감");
        }
    }
}
