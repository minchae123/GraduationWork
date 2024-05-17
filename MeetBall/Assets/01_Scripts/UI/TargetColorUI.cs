using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TargetColorUI : MonoBehaviour, IPointerClickHandler
{
    private bool IsClick = false;
    private DescriptionPanel panel;

    private void Awake()
    {
        panel = transform.GetComponentInChildren<DescriptionPanel>();    
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        IsClick = !IsClick;
        if(IsClick)
        {
            panel.OpenPanel();
        }
        else
        {
            panel.ClosePanel();
        }
    }
}
