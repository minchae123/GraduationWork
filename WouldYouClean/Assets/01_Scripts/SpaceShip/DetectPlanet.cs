using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlanet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            collision.gameObject.GetComponent<PlanetInSpace>()._isDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            collision.gameObject.GetComponent<PlanetInSpace>()._isDetected = false;
        }
    }
}
