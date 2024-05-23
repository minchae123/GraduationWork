using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private Image lockImage;

    private bool canPlay = false;

    private bool isSelect = false;
    public bool IsSelect => isSelect;

    private Vector3 originLockPos;

    public void SetUI(int stage, bool clear)
    {
        stageText.text = stage.ToString("D2");
        lockImage.gameObject.SetActive(!clear);

        canPlay = clear;
        originLockPos = lockImage.transform.localPosition;
    }

    public bool CheckCanPlay()
    {
        if (canPlay)
        {
            return true;
        }

        lockImage.rectTransform.localPosition = originLockPos;
        lockImage.transform.DOShakePosition(0.5f, strength: new Vector3(5, 5, 0), vibrato: 10, randomness: 90, fadeOut: false).SetUpdate(true);
        return false;
    }

    public void Selected()
    {
        transform.DOScale(2f, 0.6f);
        isSelect = true;
    }
    public void UnSelected()
    {
        transform.DOScale(1f, 0.6f);
        isSelect = false;
    }
}
