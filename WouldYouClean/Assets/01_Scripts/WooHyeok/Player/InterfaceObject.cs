using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceObject : PlayerMain
{
    private string _objTypeName;

    [SerializeField] private DivideObj _cleanItem;

    private RectTransform _dictionalyRect;

    [Header("UI")]
    [SerializeField] private GameObject _dictionaly;

    public void Update()
    {
        if (_isAnim)
        {
            _animator.SetTrigger("pick");
            _isAnim = false;
            StartCoroutine(PickDelay());
        }

        if (_isQKeyDown)
        {
            UIManager.Instance.ShowPanel(_dictionalyRect);
        }
    }

    IEnumerator PickDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("S");
        CleanItem();
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

            _isFKeyDown = false;
            _isAnim = true;
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
