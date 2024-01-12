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
            // ���ο� ������Ʈ�� ��ġ�� ã�� ������ �ݺ�
            Vector3 spawnPosition = GetValidRandomPosition();

            GameObject newGarbage = Instantiate(_garbagePrefab, spawnPosition, Quaternion.identity);
            _spawnedPositions.Add(spawnPosition);
            // ���� _garbage�� �θ� �����ؾ� �Ѵٸ� �Ʒ� �ּ��� �����ϼ���.
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
            if (distance < 2.0f) // ������ �Ÿ�
            {
                print("true");
                return true;
            }
        }
        return false;
    }

}
