using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OneStepCube : MapCube
{
    private void Start()
    {
        gameObject.layer = 6;
        //디졸브 초기화
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DissolveWall());
        print("나가기");
    }

    private IEnumerator DissolveWall()
    {
        //디졸브
        yield return new WaitForSeconds(1);
        gameObject.layer = 0;
    }
}//
