namespace Planetarity
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    // todo: unite signals with player input system
    public class InactivityChecker : MonoBehaviour
    {
        [SerializeField] private float inactivityTime = 60;

        private bool isCheck = true;

        private float timeSinceLastAction = 0;

        private InputMaster input;
        private Level.Level level;

        private void Awake()
        {
            input = new InputMaster();
            level = FindObjectOfType<Level.Level>();

            input.Player.Aim.performed += HandleInput;
            input.Player.Shoot.performed += HandleInput;

            input.Player.Aim.canceled += CancelInput;
            input.Player.Shoot.canceled += CancelInput;
        }

        private void HandleInput(InputAction.CallbackContext context)
        {
            timeSinceLastAction = 0;
            isCheck = false;
        }

        private void CancelInput(InputAction.CallbackContext context)
        {
            timeSinceLastAction = 0;
            isCheck = true;
        }

        private void Update()
        {
            timeSinceLastAction += Time.deltaTime;

            if (timeSinceLastAction > inactivityTime)
            {
                level.Fail();
            }
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
            input.Player.Aim.performed -= HandleInput;
            input.Player.Shoot.performed -= HandleInput;

            input.Player.Aim.canceled -= CancelInput;
            input.Player.Shoot.canceled -= CancelInput;
        }
    }
}