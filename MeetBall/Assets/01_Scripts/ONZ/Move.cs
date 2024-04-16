using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VecMap
{
    public int X;
    public int Y;
}

public class Move : MonoBehaviour
{
    //[SerializeField] private List<VecZ> vec;

    [SerializeField] private List<VecMap> map;

    KeyValuePair<int, VecMap> current;
}
