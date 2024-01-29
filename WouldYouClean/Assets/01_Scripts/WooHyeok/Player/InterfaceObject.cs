using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterfaceObject : PlayerMain
{
    private string _objTypeName;
    private string _objTypeExplain;
    private Vector3 _spawnPosition;

    private bool _isShowing = true;
    private RectTransform _panelRect;
    private RectTransform _dictionalyRect;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _explainText;
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _dictionaly;

    private void Awake()
    {
        _panelRect = _panel.GetComponent<RectTransform>();
        _dictionalyRect = _dictionaly.GetComponent<RectTransform>();
    }

    public void Update()
    {
        if (_panelRect.localScale == Vector3.zero)
            _isShowing = true;

        if (_isQKeyDown)
        {
            ShowPanel(_dictionalyRect);
        }
    }

    private void ShowPanel(RectTransform panel)
    {
        panel.transform.DOScale(Vector3.one * 1f, 1.5f); // 크기를 1.5배로 1초 동안 점차 키움
        panel.DOAnchorPos(Vector3.zero, 1.5f);
    }

    public void ClosePanel(RectTransform rect)
    {
        rect.transform.DOScale(Vector3.zero, 1f);
        rect.DOAnchorPos(_spawnPosition, 1f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DivideObj obj))
        {
            KeyDown();

            _objTypeName = obj.type._ObjectName;
            _objTypeExplain = obj.type._ObjectExplain;

            if (_isShowing)
                ColisionPos(collision.transform.position);
        }
    }

    private void ColisionPos(Vector2 pos)
    {
        _spawnPosition = pos;

        _spawnPosition = new Vector3(_spawnPosition.x, _spawnPosition.y, Camera.main.transform.position.z);

        Vector3 panelPosition = Camera.main.ViewportToWorldPoint(_spawnPosition);
        _panel.GetComponent<RectTransform>().position = panelPosition;
    }

    private void KeyDown()
    {
        if (_isFKeyDown && _isShowing && !_isQKeyDown)
        {
            if (_objTypeName == "plain")
                _isPlain = true;
            else
                _isPlain = false;

            _nameText.text = _objTypeName;
            _explainText.text = _objTypeExplain;

            _isFKeyDown = false;
            _isShowing = false;

            ShowPanel(_panelRect);
        }
    }
}
