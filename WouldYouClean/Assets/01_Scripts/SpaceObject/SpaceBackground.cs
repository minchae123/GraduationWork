using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceBackground : MonoBehaviour
{
    private SpriteRenderer _sr;
    private Spaceship _spaceship;
    private Quaternion rot;

    public float bx;
    public float by;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _spaceship = GameObject.Find("Spaceship").GetComponent<Spaceship>();
    }

    void Update()
    {
        _sr.material.SetVector("_Offset", new Vector2(bx, by));

        rot = _spaceship.transform.rotation;
        
        if (rot.z < 60 && rot.z > 300)
            by += 1f * Time.deltaTime;

        //if (rot.z > 120 && rot.z < 240)
        //    by -= 1f * Time.deltaTime;

        //if (rot.z > 210 && rot.z < 330)
        //    bx += 1f * Time.deltaTime;

        //if (rot.z > 30 && rot.z < 150)
        //    bx -= 1f * Time.deltaTime;
    }
}
