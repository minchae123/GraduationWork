using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
    [SerializeField] DivideObj _garbagePrefab;


    public int GarbageCount;
    List<Vector3> _spawnedPositions = new List<Vector3>();

    private void Start()
    {
        GarbageCount = Random.Range(MapManager.Instance.MinInclusive, MapManager.Instance.MaxInclusive);

        for (int i = 0; i < GarbageCount; i++)
        {
            // ���ο� ������Ʈ�� ��ġ�� ã�� ������ �ݺ�
            Vector3 spawnPosition = GetRandomPosition();

            GameObject newGarbage = Instantiate(_garbagePrefab.gameObject, spawnPosition, Quaternion.identity);
            _spawnedPositions.Add(spawnPosition);

            MapManager.Instance.CurrentMapTrash.Add(_garbagePrefab);

            newGarbage.transform.parent = transform;
        }

        Debug.Log("Generated " + GarbageCount + " garbage objects.");
    }

    private Vector3 GetRandomPosition()
    {
        float x, y;
        Vector3 spawnPosition;

        do
        {
            x = Random.Range(-transform.localScale.x / 2f, transform.localScale.x / 2f);
            y = Random.Range(-transform.localScale.y / 2f, transform.localScale.y / 2f);

            spawnPosition = transform.position + new Vector3(x, y, 0);
        } while (IsPositionOverlapping(spawnPosition));

        return spawnPosition;
    }

    private bool IsPositionOverlapping(Vector3 position) // ��ġ���� Ȯ��
    {
        foreach (Vector3 spawnedPosition in _spawnedPositions)
        {
            float distance = Vector3.Distance(position, spawnedPosition);
            if (distance < 2.0f) // ������ �Ÿ�
            {
                return true;
            }
        }
        return false;
    }

}
