using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public int count;

    private RaycastHit hit;
    private Ray[] ray = new Ray[6];

    private void Start()
    {
        //상하좌우앞뒤
        ray[0].direction = transform.up;
        ray[1].direction = -transform.up;
        ray[2].direction = -transform.right;
        ray[3].direction = transform.right;
        ray[4].direction = transform.forward;
        ray[5].direction = -transform.forward;
    }


    void Update()
    {
        for (int i = 0; i < ray.Length; i++)
        {
            Debug.DrawRay(transform.position, ray[i].direction);

            if (Physics.Raycast(ray[i], out hit, 1))
            {
                CheckBlock(hit.collider.gameObject);
            }
        }

        Move();
    }

    private void CheckBlock(GameObject obj)
    {
        if (obj.CompareTag("Moveable"))
            obj.GetComponent<MoveableBlock>().canMove = true;
    }

    private void Move()
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
