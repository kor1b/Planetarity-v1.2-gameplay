using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    private InputMaster _input;

    public float AimInputDirection { get; set; }

    public delegate void OnInputChanged();

    public OnInputChanged aimInputHandleEventHandler;
    public OnInputChanged aimInputCancelEventHandler;

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
        AimInputDirection = _input.Player.Aim.ReadValue<float>();
        
        aimInputHandleEventHandler?.Invoke();
    }

    private void CancelAimInput(InputAction.CallbackContext context)
    {
        AimInputDirection = 0;
        
        aimInputCancelEventHandler?.Invoke();
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