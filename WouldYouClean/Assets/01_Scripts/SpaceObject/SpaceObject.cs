using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    protected Rigidbody2D _rb;
    public int damage;

    public void SetDir(Vector2 dir)
    {
        _rb.velocity = dir;
    }
}
