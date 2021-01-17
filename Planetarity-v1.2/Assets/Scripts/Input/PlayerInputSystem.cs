namespace Planetarity.Input
{
    using UnityEngine.InputSystem;

    public class PlayerInputSystem : CharacterInputSystem
    {
        public InputMaster Input { get; private set; }

        private void Awake()
        {
            Input = new InputMaster();

            SubscribeOnPlayerAimInput();
            SubscribeOnPlayerShootInput();
        }

        // TODO: replace many subscribes to abstract method
        private void SubscribeOnPlayerAimInput()
        {
            Input.Player.Aim.performed += HandleAimInput;
            Input.Player.Aim.canceled += CancelAimInput;
        }

        private void UnsubscribeFromPlayerAimInput()
        {
            Input.Player.Aim.performed -= HandleAimInput;
            Input.Player.Aim.canceled -= CancelAimInput;
        }

        public override void HandleAimInput(InputAction.CallbackContext context)
        {
            AimInputDirection = (int) Input.Player.Aim.ReadValue<float>();

            base.HandleAimInput(context);
        }

        private void SubscribeOnPlayerShootInput()
        {
            Input.Player.Shoot.performed += HandleShootInput;
        }

        private void UnsubscribeFromPlayerShootInput()
        {
            Input.Player.Shoot.performed -= HandleShootInput;
        }

        private void OnEnable()
        {
            Input.Player.Aim.Enable();
            Input.Player.Shoot.Enable();
        }

        private void OnDisable()
        {
            Input.Player.Aim.Disable();
            Input.Player.Shoot.Disable();
        }

        private void OnDestroy()
        {
            UnsubscribeFromPlayerAimInput();
            UnsubscribeFromPlayerShootInput();
        }
    }
}