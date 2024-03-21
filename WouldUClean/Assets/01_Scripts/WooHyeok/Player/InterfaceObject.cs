using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceObject : PlayerMain
{
    private string _objTypeName;

    [SerializeField] private DivideObj _cleanItem;

    private RectTransform _dictionalyRect;
    private bool _isPick = false;
    private bool _isAnim = true;

    [Header("UI")]
    [SerializeField] private GameObject _dictionaly;

    public void Update()
    {
        if (_isPick && _isAnim)
        {
            _animator.SetTrigger("pick");
            StartCoroutine(PickDelay());

            _isAnim = false;
        }

        if (_isQKeyDown)
        {
            UIManager.Instance.ShowPanel(_dictionalyRect);
        }
    }

    IEnumerator PickDelay()
    {
        yield return new WaitForSeconds(0.5f);
        CleanItem();

        _isAnim = true;
        _isPick = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DivideObj obj))
        {
            _cleanItem = obj;
            _objTypeName = obj.type._ObjectName;

            KeyDown();
        }
    }

    private void KeyDown()
    {
        if (_isFKeyDown && !_isQKeyDown)
        {
            if (_objTypeName == "plain")
                _isPlain = true;
            else
                _isPlain = false;

            if (!_isPick)
                _isPick = true;
            //ShowPanel(_panelRect); //ÆÇ³Ú ¶ç¿ì±â
        }
    }

    public void CleanItem(/*RectTransform rect*/)
    {
        CollectedPlanets.Instance.AddTrashCollected(_cleanItem);//µµ°¨¿¡ Ãß°¡
        _cleanItem.PickUpItem();

        //ClosePanel(rect);
    }
}
