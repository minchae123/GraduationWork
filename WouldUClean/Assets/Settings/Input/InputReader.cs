using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private PlayerInputs _inputAction;

    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnMousePos;
    public event Action<bool> OnDivide;
    public event Action<bool> OnDictionary;
    public event Action OnMouseClick;

    void Start()
    {
        _inputAction = new PlayerInputs();
        _inputAction.Player.Enable();

        //_inputAction.Player.Divide.performed += OnKeyFHandle;
        _inputAction.Player.MouseClick.performed += OnLeftMouseClickHandle;
    }

    private void OnLeftMouseClickHandle(InputAction.CallbackContext context)
    {
        OnMouseClick?.Invoke();
    }

    void Update()
    {
        Vector2 inputDir = _inputAction.Player.Move.ReadValue<Vector2>();
        OnMove.Invoke(inputDir);

        Vector2 dir = _inputAction.Player.MousePos.ReadValue<Vector2>();
        OnMousePos?.Invoke(dir);

        bool isFclick = _inputAction.Player.Divide.IsPressed();
        bool isQclick = _inputAction.Player.Dictionaly.IsPressed();

        if (isFclick)
            OnDivide?.Invoke(true);
        else
            OnDivide?.Invoke(false);
        
        if (isQclick)
            OnDictionary?.Invoke(true);
        else
            OnDictionary?.Invoke(false);
    }
}
