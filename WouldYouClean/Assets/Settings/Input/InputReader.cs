using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private PlayerInput _inputAction;

    public event Action<Vector2> OnMovement;
    public event Action<Vector2> OnMousePos;
    public event Action<bool> OnFKeyDown;
    public event Action OnLeftMouseClick;

    void Start()
    {
        _inputAction = new PlayerInput();
        _inputAction.Player.Enable();

        //_inputAction.Player.Divide.performed += OnKeyFHandle;
        _inputAction.Player.MouseClick.performed += OnLeftMouseClickHandle;
    }

    //private void OnKeyFHandle(InputAction.CallbackContext context)
    //{
    //    if (context.started) OnFKeyDown?.Invoke(true);
    //    else OnFKeyDown?.Invoke(false);
    //}

    private void OnLeftMouseClickHandle(InputAction.CallbackContext context)
    {
        OnLeftMouseClick?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputDir = _inputAction.Player.Move.ReadValue<Vector2>();
        OnMovement.Invoke(inputDir);

        Vector2 dir = _inputAction.Player.MousePos.ReadValue<Vector2>();
        OnMousePos?.Invoke(dir);

        bool isclick = _inputAction.Player.Divide.IsPressed();

        if (isclick)
            OnFKeyDown?.Invoke(true);
        else
            OnFKeyDown?.Invoke(false);
    }
}
