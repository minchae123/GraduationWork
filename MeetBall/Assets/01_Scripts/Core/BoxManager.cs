using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoSingleton<BoxManager>
{
    [SerializeField] private List<Box> Boxes = new List<Box>();

    public void FindBox()
    {
        SearchBox();
           Box[] boxes = GameObject.FindObjectsOfType<Box>();

        if (boxes.Length == 0)
            Boxes.Clear();

        foreach (Box box in boxes)
        {
            if (!Boxes.Contains(box))
            {
                print(box);
                Boxes.Add(box);
            }
        }
    }

    public void ClearBox() => Boxes.Clear();
    private void SearchBox()
    {
        Box box = GameObject.FindObjectOfType<Box>();

        if (box is null)
            Boxes.Clear();
    }

    public Box ReturnBox(Vector3 player)
    {
        SearchBox();

        if (Boxes.Count == 0)
            return null;

        Box nealBox = Boxes[0];

        foreach (Box box in Boxes)
        {
            if(box is null) return null;

            float saveDis = Vector3.Distance(nealBox.transform.position, player);
            float newDis = Vector3.Distance(box.transform.position, player);

            if (newDis < saveDis)
                nealBox = box;
        }

        return nealBox;
    }
}
