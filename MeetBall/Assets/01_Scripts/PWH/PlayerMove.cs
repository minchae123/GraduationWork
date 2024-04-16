using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IPlayerControl
{
    public void Move(Vector3 targetPos)
    {
        Vector3.MoveTowards(transform.position, targetPos, 3);
    }
}
