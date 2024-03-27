using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
    [SerializeField] DivideObj _garbagePrefab;

    public int GarbageCount;
    List<Vector3> _spawnedPositions = new List<Vector3>();
    public List<ObjectType> TrashTypeList = new List<ObjectType>();
    private float mapRadius = 30f; // �� ũ�⸦ ���߿� �־�� �� ���� �ֽ��ϴ�.

    private void Start()
    {
        GarbageCount = Random.Range(MapManager.Instance.MinInclusive, MapManager.Instance.MaxInclusive);

        for (int i = 0; i < GarbageCount; i++)
        {
            // ���ο� ������Ʈ�� ��ġ�� ã�� ������ �ݺ�
            Vector3 spawnPosition = GetRandomPositionInCircle();

            GameObject newGarbage = Instantiate(_garbagePrefab.gameObject, spawnPosition, Quaternion.identity);

            newGarbage.GetComponent<DivideObj>().type = TrashTypeList[Random.Range(0, TrashTypeList.Count)];
            _spawnedPositions.Add(spawnPosition);

            MapManager.Instance.CurrentMapTrash.Add(_garbagePrefab);

            newGarbage.transform.parent = transform;
            //Debug.Log("Distance: " + Vector3.Distance(Vector3.zero, spawnPosition) + " " + i);
        }

        //Debug.Log("Generated " + GarbageCount + " garbage objects.");
    }

    private Vector3 GetRandomPositionInCircle()
    {
        float radius = Mathf.Sqrt(Random.Range(0f, 1f)) * mapRadius;
        float angle = Random.Range(0f, Mathf.PI * 2f);

        float x = radius * Mathf.Cos(angle);
        float y = radius * Mathf.Sin(angle);

        return transform.position + new Vector3(x, y, 0);
    }
}
