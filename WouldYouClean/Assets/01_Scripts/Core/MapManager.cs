using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoSingleton<MapManager>
{
    public Transform Map;

    public int MinInclusive;
    public int MaxInclusive;
    //���� ����?
    public List<DivideObj> CurrentMapTrash;

    public void RemoveTrash(DivideObj item)
    {
        CurrentMapTrash.Remove(item);
    }

    public void UpdateTrashList()
    {
        CurrentMapTrash.Clear();

        int childCount = Map.childCount;


        // ��� �ڽ� ������Ʈ�� ����Ʈ�� �߰�
        for (int i = 0; i < childCount; i++)
        {
            CurrentMapTrash.Add(Map.GetChild(i).GetComponent<DivideObj>());
            print(Map.GetChild(i).GetComponent<DivideObj>());
        }
    }
}
