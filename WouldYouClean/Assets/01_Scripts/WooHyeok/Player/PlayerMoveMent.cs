using TMPro;
using DG.Tweening;
using UnityEngine;

public class PlayerMoveMent : PlayerMain
{
    [SerializeField] private InputReader _input;
    [SerializeField] private float _curSpeed;
    [SerializeField] private TextMeshPro _warning;

    private Vector2 _direction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _input.OnMovement += OnMovement;
        _input.OnFKeyDown += OnKeyDown;
        _input.OnMousePos += OnMousePos;
        _input.OnLeftMouseClick += OnLeftMouseClick;
    }

    void Update()
    {
        _rb.velocity = _direction * _curSpeed;
    }

    private void OnKeyDown(bool value)
    {
        if (value == true)
            _isKeyDown = true;
        else
            _isKeyDown = false;
    }

    private void OnMovement(Vector2 value)
    {
        _direction = value;
        MoveAnim();
    }

    private void MoveAnim()
    {
        _animator.SetFloat("x", Mathf.Abs(_direction.x) / _curSpeed);

        if (Mathf.Abs(_direction.x) / _curSpeed == 0)
            _animator.SetFloat("x", Mathf.Abs(_direction.y) / _curSpeed);
    }

    private void OnMousePos(Vector2 value)
    {

    }

    private void OnLeftMouseClick()
    {
        print("마우스 클릭 되었달ㅇ통래ㅑㄴ");
    }

    public void OnStillWarning()
    {
        //Sequence seq = DOTween.Sequence();
        _warning.DOFade(1, 0.5f).SetLoops(5, LoopType.Yoyo).SetEase(Ease.InOutCirc).From();
    }
}
