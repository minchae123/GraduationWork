using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterfaceObject : PlayerMain
{
    private ObjectType _objType;

    [SerializeField] private TextMeshProUGUI _text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isKeyDown)
        {
            _objType = collision.gameObject.GetComponent<ObjectType>();

            Debug.Log(_objType._ObjectName);
            _text.text = _objType._ObjectName;

            _isKeyDown = false;
        }
    }
}
