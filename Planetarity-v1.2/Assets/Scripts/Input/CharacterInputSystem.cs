namespace Planetarity.Input
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    public abstract class CharacterInputSystem : MonoBehaviour
    {
        public delegate void OnInput();

        public OnInput AimEventHandler;
        public OnInput ShootEventHandler;

        private int aimInputDirection;
        public int AimInputDirection
        {
            get => aimInputDirection;
            set
            {
                if (value > 1)
                    aimInputDirection = 1;
                else if (value < -1)
                    aimInputDirection = -1;
                else
                    aimInputDirection = value;
            }
        }
        
        // TODO: remove context from parameters and set it only in player
        protected virtual void HandleAimInput(InputAction.CallbackContext context)
        {
            AimEventHandler?.Invoke();
        }

        protected virtual void CancelAimInput(InputAction.CallbackContext context)
        {
            AimInputDirection = 0;
        }
        
        protected virtual void HandleShootInput(InputAction.CallbackContext context)
        {
            ShootEventHandler?.Invoke();
        }
    }
}