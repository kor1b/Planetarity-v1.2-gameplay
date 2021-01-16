namespace Planetarity
{
    using System;
    using Input;
    using UnityEditor.UIElements;
    using UnityEngine;

    public class EnemyBattleLogic : MonoBehaviour
    {
        [HideInInspector] public Transform enemy; // Enemy of this character

        private CharacterInputSystem inputSystem;
        private CharacterAim aim;

        private void Awake()
        {
            inputSystem = GetComponent<CharacterInputSystem>();
            aim = GetComponent<CharacterAim>();
        }

        private void Update()
        {
            AimTick();
        }

        private void AimTick()
        {
            var directionToEnemy = enemy.transform.position - transform.position;

            var aimDirection =  transform.position - aim.origin.position;

            var angleToRotate = Vector3.SignedAngle(aimDirection, directionToEnemy, Vector3.forward);

            if (Mathf.Abs(angleToRotate) > 1)
            {
                var sideToAim = (int)Mathf.Sign(angleToRotate);

                inputSystem.AimInputDirection = sideToAim;
                
                return;
            }

            inputSystem.AimInputDirection = 0;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            
            if (enemy != null)
                Gizmos.DrawLine(transform.position, enemy.transform.position);
        }
    }
}