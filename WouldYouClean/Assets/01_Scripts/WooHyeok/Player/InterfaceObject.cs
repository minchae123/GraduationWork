using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterfaceObject : PlayerMain
{
    private string _objTypeName;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _panel;

    private void Update()
    {
        if (_isKeyDown)
        {
            _text.text = _objTypeName;

            _isKeyDown = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DivideObj obj))
        {
            _objTypeName = obj.type._ObjectName;
        }
    }
}
