using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBackground : MonoBehaviour
{
    private SpriteRenderer _sr;
    private Spaceship _spaceship;

    private float _offset = 0;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _spaceship = GameObject.Find("SpaceShip").GetComponent<Spaceship>();
    }

    void Update()
    {
        _offset += 1 * Time.deltaTime;
        //_sr.material.SetFloat("_Rotation", _offset);

        _sr.material.SetVector("_Offset", _spaceship.transform.up * _offset);
    }
}
