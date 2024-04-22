using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoSingleton<BoxManager>
{
    [SerializeField] private List<Box> Boxes = new List<Box>();

    public void FindBox()
    {
        Box[] boxes = GameObject.FindObjectsOfType<Box>();

        Boxes.Clear();

        foreach (Box box in boxes)
        {
            if (!Boxes.Contains(box) && box != null)
                Boxes.Add(box);
        }
    }

    public void RemoveBox(Box box) => Boxes.Remove(box);

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
            if (box is null) Boxes.Remove(box);

            float saveDis = Vector3.Distance(nealBox.transform.position, player);
            float newDis = Vector3.Distance(box.transform.position, player);

            if (newDis < saveDis)
                nealBox = box;
        }

        return nealBox;
    }
}
