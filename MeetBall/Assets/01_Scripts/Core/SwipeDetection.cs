using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetection : MonoSingleton<SwipeDetection>
{
    public delegate void Swipe(Vector2 direction);

    public event Swipe swipePerformed;

    [SerializeField] private InputAction position, press;

    [SerializeField] private float swipeResistance = 100;

    private Vector3 initialPos;
    private Vector3 currentPos => position.ReadValue<Vector2>();

    Vector2 direction;

    private void Awake()
    {
        position.Enable();
        press.Enable();
        press.performed += _ => { initialPos = currentPos; };
        press.canceled += _ => DetectSwipe();
    }

    private void Update()
    {
    }

    //private void DetectSwipe()
    //{
    //    Vector2 delta = currentPos - initialPos;
    //    Vector2 direction = Vector2.zero;

    //    print(Mathf.Abs(delta.x));
    //    if (Mathf.Abs(delta.x) > swipeResistance)
    //    {
    //        direction.x = Mathf.Sign(delta.x); // �������� ������ ��ȣ�� ����Ͽ� ������ �����մϴ�.
    //    }
    //    if (Mathf.Abs(delta.y) > swipeResistance)
    //    {
    //        direction.y = Mathf.Sign(delta.y);
    //    }

    //    if (direction != Vector2.zero && swipePerformed != null)
    //    {
    //        swipePerformed(direction); // ������ Swipe �븮�ڿ� �����մϴ�.
    //    }
    //    print(direction);
    //}

    private void DetectSwipe()
    {
        Vector2 delta = currentPos - initialPos;
        direction = Vector2.zero;

        float deltaX = Mathf.Abs(delta.x), deltaY = Mathf.Abs(delta.y);
        print("x:" + deltaX + "y:" + deltaY);
        if (deltaX > deltaY) // x��ȭ���� y���� Ŭ �� x�� �ٲٱ�
        {
            if (deltaX > swipeResistance)
            {
                //direction.x = Mathf.Clamp(delta.x, -1, 1); // ��� ��Ÿ�� ���� ������ ���� �ٲ��
                direction.x = delta.x;
            }
        }
        if (deltaX < deltaY)
        {
            if (deltaY > swipeResistance)
            {
                //direction.y = Mathf.Clamp(delta.y, -1, 1);
                direction.y = delta.y;
            }
        }
        if (direction != Vector2.zero & swipePerformed != null)
            swipePerformed(direction);
    }
}
