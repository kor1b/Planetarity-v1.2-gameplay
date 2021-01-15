using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    private InputMaster _input;

    public int AimInputDirection { get; private set; }

    public delegate void OnInputChanged();

    public OnInputChanged aimInputEventHandler;
    
    private void Awake()
    {
        _input = new InputMaster();

        SubscribeOnPlayerAimInput();
    }

    private void SubscribeOnPlayerAimInput()
    {
        _input.Player.Aim.performed += HandleAimInput;
        _input.Player.Aim.canceled += CancelAimInput;
    }

    private void UnsubscribeOnPlayerAimInput()
    {
        _input.Player.Aim.performed -= HandleAimInput;
        _input.Player.Aim.canceled -= CancelAimInput;
    }

    private void HandleAimInput(InputAction.CallbackContext context)
    {
        AimInputDirection = (int)_input.Player.Aim.ReadValue<float>();
        
        aimInputEventHandler?.Invoke();
    }

    private void CancelAimInput(InputAction.CallbackContext context)
    {
        AimInputDirection = 0;
    }

    private void OnEnable()
    {
        _input.Player.Aim.Enable();
    }

    private void OnDisable()
    {
        _input.Player.Aim.Disable();
    }

    private void OnDestroy()
    {
        UnsubscribeOnPlayerAimInput();
    }
}