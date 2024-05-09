using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TargetColorUI : MonoBehaviour, IPointerClickHandler
{
    private bool IsClick = false;

    private Transform needTrm;
    private Image[] needImage;

    private void Start()
    {
        needTrm = transform.Find("NeedColor");
        needImage = needTrm.GetComponentsInChildren<Image>();

        SetImages(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        IsClick = !IsClick;
        if (IsClick)
        {
            SetImages(true);

            Color targetC = StageManager.Instance.CurrentStageSO.targetColor;

            int cnt = 0;

            if (targetC.r > 0)
            {
                needImage[cnt++].color = Color.red;
            }
            if (targetC.g > 0)
            {
                needImage[cnt++].color = Color.green;
            }
            if (targetC.b > 0)
            {
                needImage[cnt++].color = Color.blue;
            }

            if (needImage.Length > cnt) // 3 2
            {
                for (int i = cnt; i < needImage.Length; ++i)
                {
                    needImage[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            SetImages(false);
        }
    }

    public void SetImages(bool value)
    {
        for (int i = 0; i < needImage.Length; ++i)
        {
            needImage[i].gameObject.SetActive(value);
        }

        needTrm.gameObject.SetActive(value);
    }
}
