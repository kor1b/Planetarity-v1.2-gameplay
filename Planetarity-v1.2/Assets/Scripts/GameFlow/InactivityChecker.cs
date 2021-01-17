namespace Planetarity
{
    using Input;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using LevelBased;

    public class InactivityChecker : MonoBehaviour
    {
        [SerializeField] private float inactivityTime = 60;

        private bool isCheck = true;

        private float timeSinceLastAction = 0;

        private PlayerInputSystem playerInputSystem;
        private InputMaster input;
        private Level level;

        private void Awake()
        {
            level = FindObjectOfType<Level>();
        }

        private void Start()
        {
            playerInputSystem = FindObjectOfType<PlayerInputSystem>();
            input = playerInputSystem.Input;

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
            if (!isCheck) return;

            timeSinceLastAction += Time.deltaTime;

            if (timeSinceLastAction > inactivityTime)
            {
                level.Fail();
            }
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