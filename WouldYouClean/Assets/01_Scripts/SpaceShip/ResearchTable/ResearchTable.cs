using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResearchTable : PlayerMain
{
    [SerializeField] RectTransform _mainPanel;
    //[SerializeField] TMP_Dropdown _curTrashDropdown;
    [SerializeField] TMP_Text _resultText;
    [SerializeField] List<ObjectType> _curTrash;


    [SerializeField] RectTransform _loadingPanel;
    [SerializeField] Image _loadingFillAmountIMG;
    [SerializeField] private float loadingDuration = 3.0f;

    [SerializeField] RectTransform _resultPanel;

    Vector3 _originPanelTrm;

    private void Awake()
    {
        //_curTrashDropdown.ClearOptions();

        //List<string> trashNames = new List<string>();
        //foreach (ObjectType trash in _curTrash)
        //{
        //    trashNames.Add(trash._ObjectName);
        //}

        //_curTrashDropdown.AddOptions(trashNames);
    }

    private void Start()
    {
        _originPanelTrm = _mainPanel.localScale;
        _mainPanel.localScale = Vector3.zero;
        _loadingPanel.localScale = Vector3.zero;
        _resultPanel.localScale = Vector3.zero;

        ResetLoadingFillAmount();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isKeyDown)
        {
            ActivateMainPanel();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //_mainPanel.SetActive(false);
        ExitMainPanel();
        ExitLoadingPanel();
        ExitResultPanel();
        _isKeyDown = false;
    }

    public void ResearchBtn()
    {
        //로딩하고 결과로 하기ㅣㅣ
        //_resultText.text = _curTrash[_curTrashDropdown.value]._ObjectName;
        //UIManager.Instance.ScaleRectTransform(_loadingPanel, _originPanelTrm, 0.7f, Ease.InOutCubic, FillLoadingBar);
        ActivateLoadingPanel(FillLoadingBar);
    }

    public void BackBtn()
    {
        ExitResultPanel();
    }

    private void FillLoadingBar()
    {
        _loadingFillAmountIMG.DOFillAmount(1.0f, loadingDuration).OnComplete(() =>
        {
            ExitLoadingPanel();
            ActivateResultPanel();
        });
    }

    private void ResetLoadingFillAmount()
    {
        _loadingFillAmountIMG.fillAmount = 0f;
    }

    private void ActivateMainPanel(params Action[] action)
    {
        UIManager.Instance.ScaleRectTransform(_mainPanel, _originPanelTrm
            , 0.7f, Ease.InOutCubic, action);
    }

    private void ActivateLoadingPanel(params Action[] action)
    {
        UIManager.Instance.ScaleRectTransform(_loadingPanel, _originPanelTrm
            , 0.7f, Ease.InOutCubic, action);
    }

    private void ActivateResultPanel(params Action[] action)
    {
        UIManager.Instance.ScaleRectTransform(_resultPanel, _originPanelTrm
            , 0.0f, Ease.InOutCubic, action);
    }

    public void ExitMainPanel(params Action[] action)
    {
        UIManager.Instance.ScaleRectTransform(_mainPanel, Vector3.zero
            , 0.7f, Ease.InOutCubic, action);
    }

    public void ExitLoadingPanel(params Action[] action)
    {
        UIManager.Instance.ScaleRectTransform(_loadingPanel, Vector3.zero
            , 0.7f, Ease.InOutCubic, action);
    }

    public void ExitResultPanel(params Action[] action)
    {
        UIManager.Instance.ScaleRectTransform(_resultPanel, Vector3.zero
            , 0.7f, Ease.InOutCubic, action);
    }
}
