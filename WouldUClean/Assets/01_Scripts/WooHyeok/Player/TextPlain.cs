using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPlain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMain>())
            collision.GetComponent<PlayerHp>()._istest = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMain>())
            collision.GetComponent<PlayerHp>()._istest = false;
    }
}
