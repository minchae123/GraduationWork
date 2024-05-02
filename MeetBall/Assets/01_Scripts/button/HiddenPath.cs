using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HiddenPath : MonoBehaviour
{
    private void Awake()
    {
        gameObject.layer = 0;
    }

}
