using UnityEngine;

public class MapGarbageSpawner : MonoBehaviour
{
    public GameObject resourcePrefab;

    public int Count;

    private void Start()
    {
        SpawnGarbage(Count);
    }

    private void SpawnGarbage(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(resourcePrefab, RandomPos(), Quaternion.identity);
        }
    }

    private Vector3 RandomPos()
    {
        Vector3 pos;

        float x, y, z;

        x = Random.Range(-100, 0);
        y = 20;
        z = Random.Range(-40, 40);

        pos = new Vector3(x, y, z);

        return pos;
    }
}
