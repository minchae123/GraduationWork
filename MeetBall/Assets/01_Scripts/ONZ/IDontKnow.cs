using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDontKnow : MonoBehaviour
{
    [SerializeField] private Transform cubeParent;
    [SerializeField] private F[,,] cube;

    int X = 0, Z = 0, Y = 0;

    private void Awake()
    {
        cube = new F[10, 10, 10];
    }

    private void Start()
    {
        SettingMap();
    }

    private void Update()
    {
        MoveKey();
    }

    private void MoveKey()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            X++;
            Move(cube[X, Z, Y].transform.position);
        }
    }

    private void Move(Vector3 targetPos)
    {
        Vector3.MoveTowards(transform.position, targetPos, 1);
    }

    private void SettingMap()
    {
        foreach (Transform f in cubeParent)
        {
            foreach (Transform g in f)
            {
                foreach (Transform h in g)
                {
                    cube[X, Z, Y] = h.GetComponent<F>();
                    Y++;
                }
                Y = 0;
                Z++;
            }
            Z = 0;
            X++;
        }
        Y = 0;
    }

    //private List<Transform> GetChildren(Transform parent, ref int plus, ref int reset)
    //{
    //    List<Transform> children = new List<Transform>();

    //    foreach (Transform child in parent)
    //    {
    //        cube[a, b, c] = child.GetComponent<F>();
    //        children.Add(child);

    //        plus++;
    //    }

    //    reset = 0;
    //    return children;
    //} 
    //다시 찾아온다.
}
