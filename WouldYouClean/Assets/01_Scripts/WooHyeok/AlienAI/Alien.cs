using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    private Vector2 runningDir;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        runningDir = (GameObject.Find("Player").transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = runningDir * 5;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.TryGetComponent(out ))
    }
}
