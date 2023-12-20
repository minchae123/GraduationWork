using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResearchTable : PlayerMain
{
    [SerializeField] GameObject _researchPanel;
    [SerializeField] TMP_Dropdown _curTrashDropdown;
    [SerializeField] TMP_Text _resultText;
    [SerializeField] List<ObjectType> _curTrash;


    [SerializeField] RectTransform _loadingPanel;
    [SerializeField] Image _loadingFillAmountIMG;
    [SerializeField] private float loadingDuration = 3.0f;

    [SerializeField] GameObject _resultPanel;

    Vector3 _originLoadingPanelTrm;

    private void Awake()
    {
        _curTrashDropdown.ClearOptions();

        List<string> trashNames = new List<string>();
        foreach (ObjectType trash in _curTrash)
        {
            trashNames.Add(trash._ObjectName);
        }

        _curTrashDropdown.AddOptions(trashNames);
    }

    private void Start()
    {
        _originLoadingPanelTrm = _loadingPanel.localScale;
        _loadingPanel.localScale = Vector3.zero;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isKeyDown)
        {
            _researchPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _researchPanel.SetActive(false);
        _isKeyDown = false;
    }

    public void ResearchBtn()
    {
        //로딩하고 결과로 하기ㅣㅣ
        _resultText.text = _curTrash[_curTrashDropdown.value]._ObjectName;
        UIManager.Instance.ScaleRectTransform(_loadingPanel, _originLoadingPanelTrm
            , 0.7f, Ease.InOutCubic, FillLoadingBar);
    }

    public void BackBtn()
    {
        _resultPanel.SetActive(false);
    }

    private void FillLoadingBar()
    {
        _loadingFillAmountIMG.DOFillAmount(1.0f, loadingDuration).OnComplete(() =>
        {
            UIManager.Instance.ScaleRectTransform(_loadingPanel, new Vector3(0,0,0), 0.7f, Ease.InOutCubic, ResetLoadingFillAmount);
        });
    }

    private void ResetLoadingFillAmount()
    {
        _loadingFillAmountIMG.fillAmount = 0f;
    }
}
