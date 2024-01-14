using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceBackground : MonoBehaviour
{
    private SpriteRenderer _sr;
    private Spaceship _spaceship;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _spaceship = GameObject.Find("Spaceship").GetComponent<Spaceship>();
    }

    void Update()
    {
        _sr.material.SetVector("_Offset", _spaceship.transform.up);
    }
}
