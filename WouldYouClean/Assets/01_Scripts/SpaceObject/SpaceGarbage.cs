using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceGarbage : SpaceObject
{
    public SpaceShip spaceShip;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        spaceShip = GameObject.Find("Spaceship").GetComponent<SpaceShip>();
    }
    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spaceship"))
        {
            collision.transform.DOShakePosition(0.2f);
            Destroy(gameObject);
            //¿¬·á»¯±â
        }
    }
}
