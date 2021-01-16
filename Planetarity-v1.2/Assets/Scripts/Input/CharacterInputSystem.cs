namespace Planetarity.Input
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    public abstract class CharacterInputSystem : MonoBehaviour
    {
        public delegate void OnInput();

        public OnInput aimEventHandler;
        public OnInput shootEventHandler;

        public int AimInputDirection { get; protected set; }
        
        protected virtual void HandleAimInput(InputAction.CallbackContext context)
        {
            aimEventHandler?.Invoke();
        }

        protected virtual void CancelAimInput(InputAction.CallbackContext context)
        {
            AimInputDirection = 0;
        }
        
        protected virtual void HandleShootInput(InputAction.CallbackContext context)
        {
            shootEventHandler?.Invoke();
        }
    }
}