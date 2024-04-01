using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGarbageSpawner : MonoBehaviour
{
    public GameObject resourcePrefab;

    public int Count;

    [SerializeField] List<GameObject> _garbage = new List<GameObject>();
    public int DestoryCount = 0;

    private void Start()
    {
        Count = Random.Range(20, 25);
    }

    private void Update()
    {
        for (int i = 0; i < _garbage.Count; i++)
        {
            if (_garbage[i] != null)
            {
                if (_garbage.Any()) // List.Any() 요소가 있으면 true
                {
                    if (_garbage[i].transform.position.y < -20) // 쓰레기 잘못 생성시 할것트 탈출시 제거하기러기러기차는길어길면바나나바나나는맛있어맛있으면사과
                    {
                        Destroy(_garbage[i]);
                        _garbage.RemoveAt(i);
                        DestoryCount++;
                        PrintDestroyCount();
                    }
                }
            }
        }

    }

    private void PrintDestroyCount()
    {
        print($"제거된 쓰레기 개수: {DestoryCount}");
    }

    public void SpawnGarbage() // 맵 활성화시 이걸하면 쓰레기가 소환돼요
    {
        for (int i = 0; i < Count; i++)
        {
            GameObject obj = Instantiate(resourcePrefab, RandomPos(), Quaternion.identity);
            _garbage.Add(obj);
        }
    }

    private Vector3 RandomPos()
    {
        Vector3 pos;

        float x, y, z;

        x = Random.Range(-50, 50);
        y = 20;
        z = Random.Range(-40, 40);

        pos = new Vector3(x, y, z);

        return pos;
    }
}
