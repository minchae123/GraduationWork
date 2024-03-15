using UnityEngine;

public class InterfaceObject : PlayerMain
{
    private string _objTypeName;
    private bool _isAnim = false;

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
        }

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
