using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private PlayerInputs _inputAction;

    public event Action<Vector3> OnMovement;
    public event Action<Vector2> OnMousePos;
    public event Action<bool> OnFKeyDown;
    public event Action<bool> OnQKeyDown;
    public event Action OnLeftMouseClick;

    void Start()
    {
        _inputAction = new PlayerInputs();
        _inputAction.Player.Enable();

        //_inputAction.Player.Divide.performed += OnKeyFHandle;
        _inputAction.Player.MouseClick.performed += OnLeftMouseClickHandle;
    }

    private void OnLeftMouseClickHandle(InputAction.CallbackContext context)
    {
        OnLeftMouseClick?.Invoke();
    }

    void Update()
    {
        Vector3 inputDir = _inputAction.Player.Move.ReadValue<Vector3>();
        OnMovement.Invoke(inputDir);

        Vector2 dir = _inputAction.Player.MousePos.ReadValue<Vector2>();
        OnMousePos?.Invoke(dir);

        bool isFclick = _inputAction.Player.Divide.IsPressed();
        bool isQclick = _inputAction.Player.Dictionaly.IsPressed();

        if (isFclick)
            OnFKeyDown?.Invoke(true);
        else
            OnFKeyDown?.Invoke(false);
        
        if (isQclick)
            OnQKeyDown?.Invoke(true);
        else
            OnQKeyDown?.Invoke(false);
    }
}
