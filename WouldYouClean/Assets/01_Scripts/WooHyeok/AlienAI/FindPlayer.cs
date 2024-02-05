using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer : MonoBehaviour
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
        if(collision.TryGetComponent<PlayerMain>(out PlayerMain player))
        {
            transform.parent.GetChild(1).GetComponent<Alien>()._isFollow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMain>(out PlayerMain player))
        {
            transform.parent.GetChild(1).GetComponent<Alien>()._isFollow = false;
        }
    }
}
