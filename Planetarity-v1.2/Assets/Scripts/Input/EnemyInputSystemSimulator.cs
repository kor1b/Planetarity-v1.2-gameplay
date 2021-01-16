namespace Planetarity.Input
{
    using Level;
    using UnityEngine;

    public class EnemyInputSystemSimulator : CharacterInputSystem
    {
        private Vector3 directionToEnemy;

        private CharacterAim _aim;

        private void Awake()
        {
        }

        private void Start()
        {
            AimInputDirection = 1;
        }

        private void Update()
        {
            HandleAimInput(default);
        }
    }
}