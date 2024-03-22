using TMPro;
using DG.Tweening;
using UnityEngine;

public class PlayerMoveMent : PlayerMain
{
    [SerializeField] private Camera cam;

    [SerializeField] private InputReader _input;
    [SerializeField] private TextMeshPro _warning;

    [SerializeField] private float _curSpeed;
    [SerializeField] private float maxDistance = 35;

    [HideInInspector] public Vector3 _direction;

    private float xRotate = 0;
    private float yRotate = 0;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _input.OnMovement += OnMovement;
        _input.OnFKeyDown += OnFKeyDown;
        _input.OnQKeyDown += OnQKeyDown;
        _input.OnMousePos += OnMousePos;
        _input.OnLeftMouseClick += OnLeftMouseClick;
    }

    void Update()
    {
        RotCam();
        _rb.velocity = _direction * _curSpeed;

        transform.position = Vector3.ClampMagnitude(transform.position, maxDistance);
    }

    private void OnFKeyDown(bool value)
    {
        if (value == true)
            _isFKeyDown = true;
        else
            _isFKeyDown = false;
    }

    private void OnQKeyDown(bool value)
    {
        if (value == true)
            _isQKeyDown = true;
        else
            _isQKeyDown = false;
    }

    private void OnMovement(Vector3 value)
    {
        _direction = value;
        MoveAnim();
    }

    private void MoveAnim()
    {
        _animator.SetFloat("x", Mathf.Abs(_direction.x) / _curSpeed);

        _animator.SetFloat("y", -(_direction.y / _curSpeed));
    }

    private void OnMousePos(Vector2 value)
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(value.x, value.y, Camera.main.nearClipPlane));

        //print(mouseWorldPosition.x);
        //print(Input.GetAxis("Mouse X"));

        //float mouseX = mouseWorldPosition.x;
        //float mouseY = mouseWorldPosition.y;

        //// 카메라 회전
        //transform.Rotate(-mouseY * 5, mouseX * 5, 0);

        //// 카메라의 x축 회전 제한 (-90 ~ 90도)
        //Vector3 currentRotation = transform.localEulerAngles;
        //currentRotation.x = Mathf.Clamp(currentRotation.x, -90f, 90f);
        //transform.localEulerAngles = currentRotation;
    }

    private void RotCam()
    {
        float xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * 500;
        float yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * 500;

        yRotate = cam.transform.eulerAngles.y + yRotateMove;
        //xRotate = transform.eulerAngles.x + xRotateMove; 
        xRotate = xRotate + xRotateMove;

        xRotate = Mathf.Clamp(xRotate, -90, 90); // 위, 아래 고정

        transform.eulerAngles = new Vector3(0, yRotate, 0);
        cam.transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
    }

    private void OnLeftMouseClick()
    {
        print("마우스 클릭 되었달ㅇ통래ㅑㄴ");
    }

    public void OnStillWarning()
    {
        Sequence blinkSequence = DOTween.Sequence();
        blinkSequence.Append(_warning.DOFade(1f, 0.2f).SetEase(Ease.InOutCirc).SetLoops(5))
            .AppendCallback(() => _warning.DOFade(0f, 0.2f).SetEase(Ease.InOutCirc)); // 애니메이션 끝나면 오브젝트 비활성화
    }
}
