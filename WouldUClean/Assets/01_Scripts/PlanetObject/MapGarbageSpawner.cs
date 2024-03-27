using System.Collections.Generic;
using UnityEngine;

public class MapGarbageSpawner : MonoBehaviour
{
    public GameObject resourcePrefab;

    public int Count;

    [SerializeField] List<GameObject> _garbage = new List<GameObject>();

    private void Start()
    {
        SpawnGarbage(Count);
    }

    private void Update()
    {
        for (int i = 0; i < _garbage.Count; i++)
        {
            if (_garbage[i].transform.position.y < -20)
            {
                print("¾²·¹±âÅ»Ãâ");
            }
        }
    }

    private void SpawnGarbage(int count)
    {
        for (int i = 0; i < count; i++)
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
