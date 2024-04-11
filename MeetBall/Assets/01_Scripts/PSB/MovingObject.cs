using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public int count;

    public Vector3 position;

    void Start()
    {

    }

    void Update()
    {
        if (count > 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position += Vector3.forward;
                count -= 1;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.position -= Vector3.forward;
                count -= 1;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.position += Vector3.right;
                count -= 1;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.position -= Vector3.right;
                count -= 1;
            }
        }
    }
}
