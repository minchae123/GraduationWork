using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OneStepCube : MapCube
{
    private void Start()
    {
        gameObject.layer = 6;
        //µðÁ¹ºê ÃÊ±âÈ­
    }

    private void OnCollisionExit(Collision collision)
    {
        StartCoroutine(DissolveWall());
    }

    private IEnumerator DissolveWall()
    {
        //µðÁ¹ºê
        yield return new WaitForSeconds(1);
        gameObject.layer = 0;
    }
}//
