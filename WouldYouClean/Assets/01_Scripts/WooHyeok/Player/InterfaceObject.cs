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

    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _explainText;
    [SerializeField] private GameObject _panel;

    private void Update()
    {

    }

    private void ShowPanel()
    {
        _panel.GetComponent<RectTransform>().transform.DOScale(Vector3.one * 1f, 1.5f); // 크기를 1.5배로 1초 동안 점차 키움
        _panel.GetComponent<RectTransform>().DOAnchorPos(Vector3.zero, 1.5f);
    }

    public void ClosePanel()
    {
        print("wdefghgjh,mn");
        _panel.GetComponent<RectTransform>().transform.DOScale(Vector3.zero, 1.5f);
        _panel.GetComponent<RectTransform>().DOAnchorPos(_spawnPosition, 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DivideObj obj))
        {
            _objTypeName = obj.type._ObjectName;
            _objTypeExplain = obj.type._ObjectExplain;

            ColisionPos(collision.transform.position);
            KeyDown();
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
        if (_isKeyDown)
        {
            _nameText.text = _objTypeName;
            _explainText.text = _objTypeExplain;

            _isKeyDown = false;

            ShowPanel();
        }
    }
}
