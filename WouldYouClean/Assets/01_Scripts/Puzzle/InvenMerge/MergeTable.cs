using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MergeTable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    private Image colorImage;
    [SerializeField] private Image childImage;

    private void Start()
    {
        colorImage = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        colorImage.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        colorImage.color = Color.white;
    }

    public void OnDrop(PointerEventData eventData)
    {
        //if(eventData.pointerDrag.TryGetComponent<>().sprite)
    }

    public void SetImage(Sprite image)
    {
        childImage.sprite = image;
    }
}
