using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
    [SerializeField] GameObject _garbagePrefab;
    [SerializeField] int _minInclusive;
    [SerializeField] int _maxInclusive;

    int _garbageCount;
    List<Vector3> _spawnedPositions = new List<Vector3>();

    private void Start()
    {
        _garbageCount = Random.Range(_minInclusive, _maxInclusive);

        for (int i = 0; i < _garbageCount; i++)
        {
            // 새로운 오브젝트의 위치를 찾을 때까지 반복
            Vector3 spawnPosition = GetValidRandomPosition();

            GameObject newGarbage = Instantiate(_garbagePrefab, spawnPosition, Quaternion.identity);
            _spawnedPositions.Add(spawnPosition);
            // 만약 _garbage의 부모를 지정해야 한다면 아래 주석을 해제하세요.
            newGarbage.transform.parent = transform;
        }

        Debug.Log("Generated " + _garbageCount + " garbage objects.");
    }

    private Vector3 GetValidRandomPosition()
    {
        float x, y, z;
        Vector3 spawnPosition;

        do
        {
            x = Random.Range(-transform.localScale.x / 2f, transform.localScale.x / 2f);
            y = Random.Range(-transform.localScale.y / 2f, transform.localScale.y / 2f);

            spawnPosition = transform.position + new Vector3(x, y, 0);
        } while (IsPositionOverlapping(spawnPosition));

        return spawnPosition;
    }

    private bool IsPositionOverlapping(Vector3 position)
    {
        foreach (Vector3 spawnedPosition in _spawnedPositions)
        {
            float distance = Vector3.Distance(position, spawnedPosition);
            if (distance < 2.0f) // 떨어진 거리
            {
                print("true");
                return true;
            }
        }
        return false;
    }

}
