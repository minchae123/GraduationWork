using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterfaceObject : PlayerMain
{
    private string _objTypeName;
    private string _objTypeExplain;

    private bool _isShowing = true;

    [SerializeField] private DivideObj _cleanItem;

    private RectTransform _panelRect;
    private RectTransform _dictionalyRect;

    [Header("UI")]
    [SerializeField] private GameObject _dictionaly;

    private void Awake()
    {
        //_panelRect = _panel.GetComponent<RectTransform>();
       //_dictionalyRect = _dictionaly.GetComponent<RectTransform>();
    }

	private void Start()
	{
	}

	public void Update()
    {
        //if (_panelRect.localScale == Vector3.zero)
        //    _isShowing = true;

        if (_isQKeyDown)
        {
            UIManager.Instance.ShowPanel(_dictionalyRect);
        }
    } 

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DivideObj obj))
        {
            _cleanItem = obj;
            _objTypeName = obj.type._ObjectName;
            _objTypeExplain = obj.type._ObjectExplain;

            KeyDown();
        }
    }

    private void PickUpAnim()
    {
        //_animator.SetTrigger("pick");
        //_animator.SetBool("a", true);
    }

    private void KeyDown()
    {
        if (_isFKeyDown && !_isQKeyDown)
        {
            if (_objTypeName == "plain")
                _isPlain = true;
            else
                _isPlain = false;

            //_nameText.text = _objTypeName;
            //_explainText.text = _objTypeExplain;

            _isFKeyDown = false;
            _isShowing = false;

            PickUpAnim();
            CleanItem();
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
