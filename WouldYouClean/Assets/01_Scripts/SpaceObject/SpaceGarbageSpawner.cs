using System.Collections.Generic;
using UnityEngine;

public class SpaceGarbageSpawner : MonoBehaviour
{
    public List<ObjectType> TrashTypeList = new List<ObjectType>();
    public List<SpaceObject> SpaceTrashList = new ();
    public GameObject _garbagePrefab;

    private void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject newObj = Instantiate(_garbagePrefab.gameObject, transform);
            newObj.GetComponent<DivideObj>().type = TrashTypeList[Random.Range(0, TrashTypeList.Count)];
            SpaceTrashList.Add(newObj.GetComponent<SpaceObject>());
            newObj.transform.parent = transform;
            newObj.transform.position = (Vector3)Random.insideUnitCircle * 200 + transform.position;
        }
    }
}
