using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move1 : MonoBehaviour
{
    public int MoveCount;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position += new Vector3Int(1, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position += new Vector3Int(-1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3Int(0, -1, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3Int(0, 1, 0);
        }

    }
}
