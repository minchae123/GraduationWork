using System.Collections.Generic;
using UnityEngine;

public class SpaceGarbageSpawner : MonoBehaviour
{
    public List<ObjectType> TrashTypeList = new List<ObjectType>();
    public List<GameObject> SpaceTrashList = new List<GameObject>();
    public GameObject _garbagePrefab;

    private void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            print("dd");
            GameObject newObj = Instantiate(_garbagePrefab.gameObject, transform);
            newObj.GetComponent<DivideObj>().type = TrashTypeList[Random.Range(0, TrashTypeList.Count)];
            SpaceTrashList.Add(newObj);
            newObj.transform.parent = transform;
            newObj.transform.position = Random.insideUnitCircle * 200;
        }
    }
}
