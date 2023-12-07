using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private PlayerInput _inputAction;

    public event Action<Vector2> OnMovement;
    public event Action OnFKeyDown;

    void Start()
    {
        _inputAction = new PlayerInput();
        _inputAction.Player.Enable();

        _inputAction.Player.Divide.performed += OnKeyFHandle;
    }

    private void OnKeyFHandle(InputAction.CallbackContext context)
    {
        OnFKeyDown?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputDir = _inputAction.Player.Move.ReadValue<Vector2>();
        OnMovement.Invoke(inputDir);
    }
}
