using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArea : MonoBehaviour
{
    public bool IsReached = false;

    private void Awake()
    {
        IsReached = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CheckArea>(out CheckArea c))
        {
            IsReached = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CheckArea>(out CheckArea c))
        {
            print("Dd");
            IsReached = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //print("²ý");
        IsReached = false;
    }
}
