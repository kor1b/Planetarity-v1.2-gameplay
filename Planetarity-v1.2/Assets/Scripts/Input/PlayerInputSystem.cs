namespace Planetarity.Input
{
    using UnityEngine.InputSystem;

    public class PlayerInputSystem : CharacterInputSystem
    {
        private InputMaster input;

        private void Awake()
        {
            input = new InputMaster();

            SubscribeOnPlayerAimInput();
            SubscribeOnPlayerShootInput();
        }

        // TODO: replace many subscribes to abstract method
        private void SubscribeOnPlayerAimInput()
        {
            input.Player.Aim.performed += HandleAimInput;
            input.Player.Aim.canceled += CancelAimInput;
        }

        private void UnsubscribeFromPlayerAimInput()
        {
            input.Player.Aim.performed -= HandleAimInput;
            input.Player.Aim.canceled -= CancelAimInput;
        }

        public override void HandleAimInput(InputAction.CallbackContext context)
        {
            AimInputDirection = (int) input.Player.Aim.ReadValue<float>();

            base.HandleAimInput(context);
        }

        private void SubscribeOnPlayerShootInput()
        {
            input.Player.Shoot.performed += HandleShootInput;
        }

        private void UnsubscribeFromPlayerShootInput()
        {
            input.Player.Shoot.performed -= HandleShootInput;
        }

        private void OnEnable()
        {
            input.Player.Aim.Enable();
            input.Player.Shoot.Enable();
        }

        private void OnDisable()
        {
            input.Player.Aim.Disable();
            input.Player.Shoot.Disable();
        }

        private void OnDestroy()
        {
            UnsubscribeFromPlayerAimInput();
            UnsubscribeFromPlayerShootInput();
        }
    }
}