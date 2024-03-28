using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapInfoClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private MapInfoSO mapInfo;

    public void OnPointerClick(PointerEventData eventData)
    {
        MapInfoUI.Instance.SetSelectedMap(mapInfo);
    }
}
