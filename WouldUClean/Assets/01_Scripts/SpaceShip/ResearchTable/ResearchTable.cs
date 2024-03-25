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
    [SerializeField] ItemTestTable _testTable;
    [SerializeField] List<ObjectType> _curTrash;


    [SerializeField] RectTransform _loadingPanel;
    [SerializeField] Image _loadingFillAmountIMG;
    [SerializeField] private float loadingDuration = 3.0f;

    [SerializeField] RectTransform _resultPanel;
    [SerializeField] Image _resultImage;
    [SerializeField] TMP_Text _resultText;

    [SerializeField] List<ObjectType> _addItems;

    Vector3 _originPanelTrm;
    bool isResearchTableActive = false;

    private void Awake()
    {

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
        if (_isFKeyDown)
        {
            ActivateMainPanel();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ExitMainPanel();
        ExitLoadingPanel();
        ExitResultPanel();
        _isFKeyDown = false;
    }

    public void ResearchBtn()
    {
        if (_testTable.currentItem != null)
        {
        }
        ActivateLoadingPanel(FillLoadingBar);
    }

    public void BackBtn()
    {
        ExitResultPanel();
    }

    public void ExitBtn()
    {
        ExitMainPanel();
    }

    public void AddBtn()
    {
        if (_testTable.currentItem != null)
        {
            _addItems.Add(_testTable.currentItem);
            _testTable.currentItem = null;
            //AddItemAndDelete()만들기러길기러기러기러기러기ㅓ리거리ㅓ기ㅓ리거리거맆럭리ㅓ기러기러기러기러
        }
    }

    private void FillLoadingBar()
    {
        Tween fillTween = null;
        fillTween = _loadingFillAmountIMG.DOFillAmount(1.0f, loadingDuration).OnUpdate(() =>
        {
            if (!isResearchTableActive)
            {
                fillTween.Kill();
                fillTween = null;
            }
        }).OnComplete(() =>
        {
            ExitLoadingPanel();
            ActivateResultPanel();
        });
    }

    private void ResetLoadingFillAmount()
    {
        _loadingFillAmountIMG.fillAmount = 0f;
    }

    public void ActivateMainPanel(params Action[] action)
    {
        UIManager.Instance.ScaleRectTransform(_mainPanel, _originPanelTrm
            , 0.7f, Ease.InOutCubic, action);
        isResearchTableActive = true;
    }

    private void ActivateLoadingPanel(params Action[] action)
    {
        _loadingFillAmountIMG.fillAmount = 0f;
        UIManager.Instance.ScaleRectTransform(_loadingPanel, _originPanelTrm
            , 0.7f, Ease.InOutCubic, action);
    }

    private void ActivateResultPanel(params Action[] action)
    {
        if (isResearchTableActive)
            UIManager.Instance.ScaleRectTransform(_resultPanel, _originPanelTrm
                , 0.0f, Ease.InOutCubic, action);
        //이미지 바꾸고 텍스트 바꾸고
        _resultImage.sprite = _testTable.currentItem._ItemSprite;
        _resultText.text = _testTable.currentItem.name;


    }

    public void ExitMainPanel(params Action[] action)
    {
        if (isResearchTableActive)
        {
            Array.Resize(ref action, action.Length + 1); // 배열 크기 늘리기
            action[action.Length - 1] = RemoveTableItem; // 새로운 액션 추가

            UIManager.Instance.ScaleRectTransform(_mainPanel, Vector3.zero
                , 0.7f, Ease.InOutCubic, action);
            isResearchTableActive = false;
        }
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

    private void RemoveTableItem()
    {
        _testTable.RemoveItem();
    }

    private void DebugAction()
    {
        print("action");
    }
}
