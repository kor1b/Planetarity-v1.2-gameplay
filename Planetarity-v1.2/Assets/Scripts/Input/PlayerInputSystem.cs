namespace Planetarity.Input
{
    using UnityEngine.InputSystem;

    public class PlayerInputSystem : CharacterInputSystem
    {
        private InputMaster _input;

        private void Awake()
        {
            _input = new InputMaster();

            SubscribeOnPlayerAimInput();
            SubscribeOnPlayerShootInput();
        }

        // TODO: replace many subscribes to abstract method
        private void SubscribeOnPlayerAimInput()
        {
            _input.Player.Aim.performed += HandleAimInput;
            _input.Player.Aim.canceled += CancelAimInput;
        }

        private void UnsubscribeFromPlayerAimInput()
        {
            _input.Player.Aim.performed -= HandleAimInput;
            _input.Player.Aim.canceled -= CancelAimInput;
        }

        protected override void HandleAimInput(InputAction.CallbackContext context)
        {
            AimInputDirection = (int) _input.Player.Aim.ReadValue<float>();

            base.HandleAimInput(context);
        }

        private void SubscribeOnPlayerShootInput()
        {
            _input.Player.Shoot.performed += HandleShootInput;
        }

        private void UnsubscribeFromPlayerShootInput()
        {
            _input.Player.Shoot.performed -= HandleShootInput;
        }

        private void OnEnable()
        {
            _input.Player.Aim.Enable();
            _input.Player.Shoot.Enable();
        }

        private void OnDisable()
        {
            _input.Player.Aim.Disable();
            _input.Player.Shoot.Disable();
        }

        private void OnDestroy()
        {
            UnsubscribeFromPlayerAimInput();
            UnsubscribeFromPlayerShootInput();
        }
    }
}