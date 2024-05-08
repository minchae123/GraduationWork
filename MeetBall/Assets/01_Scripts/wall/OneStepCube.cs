using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OneStepCube : MapCube
{
    private void Start()
    {
        gameObject.layer = 6;
        //������ �ʱ�ȭ
    }

    private void OnCollisionExit(Collision collision)
    {
        StartCoroutine(DissolveWall());
    }

    private IEnumerator DissolveWall()
    {
        //������
        yield return new WaitForSeconds(1);
        gameObject.layer = 0;
    }
}//
