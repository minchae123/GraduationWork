using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class StageUI : MonoBehaviour
{
    public bool IsSelected = false;

    private void Start()
    {
        IsSelected = false;
    }

    public void Selected()
    {
        IsSelected = true;
        transform.DOScale(2f, 0.6f);
    }
    public void UnSelected()
    {
        IsSelected = false;
        transform.DOScale(1f, 0.6f);
    }
}
