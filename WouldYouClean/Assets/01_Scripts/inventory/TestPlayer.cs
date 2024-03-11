using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(h, v, 0) * Time.deltaTime * 5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print($"Ãæµ¹ : {collision.collider}");
    }
}
