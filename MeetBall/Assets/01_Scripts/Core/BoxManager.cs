using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoSingleton<BoxManager>
{
    public List<Box> Boxes = new List<Box>();

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

    public void CleanBox() => Boxes.Clear();
    public void RemoveBox(Box box) => Boxes.Remove(box);

    private void SearchBox()
    {
        Box box = GameObject.FindObjectOfType<Box>();

        if (box is null)
            Boxes.Clear();
    }

    public void boxDec(Transform player)
    {
        SearchBox();

        if (Boxes.Count == 0)
            return;

        foreach (Box box in Boxes)
        {
            if (box is null)
            {
                Boxes.Remove(box);
                continue;
            }

            if (box != null)
                box.Determine();

            if (player.TryGetComponent(out LeftControl left) && box._leftPlayerDir == left.direction)
                left.direction = Vector3.zero;
            else if (player.TryGetComponent(out RightControl right) && box._rightPlayerDir == right.direction)
                right.direction = Vector3.zero;
        }
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
            if (nealBox != null)
            {
                float saveDis = Vector3.Distance(nealBox.transform.position, player);
                float newDis = Vector3.Distance(box.transform.position, player);

                if (newDis < saveDis)
                    nealBox = box;
            }
        }

        return nealBox;
    }
}

