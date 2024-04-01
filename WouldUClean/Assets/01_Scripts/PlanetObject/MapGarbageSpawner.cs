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
                if (_garbage.Any()) // List.Any() ��Ұ� ������ true
                {
                    if (_garbage[i].transform.position.y < -20) // ������ �߸� ������ �Ұ�Ʈ Ż��� �����ϱⷯ�ⷯ�����±����ٳ����ٳ����¸��־����������
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
        print($"���ŵ� ������ ����: {DestoryCount}");
    }

    public void SpawnGarbage() // �� Ȱ��ȭ�� �̰��ϸ� �����Ⱑ ��ȯ�ſ�
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
