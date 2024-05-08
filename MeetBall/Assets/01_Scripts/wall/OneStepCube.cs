using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OneStepCube : MapCube
{
    private void Update()
    {
        if (isVisit)
        {
            gameObject.layer = 0;
        }
    }
}
