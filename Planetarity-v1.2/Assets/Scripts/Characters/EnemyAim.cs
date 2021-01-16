namespace Planetarity
{
    using System;
    using UnityEngine;

    [RequireComponent(typeof(EnemyBattleLogic))]
    public class EnemyAim : CharacterAim
    {
        public Transform enemy;

        public bool isAimed = false;

        protected override void CalculateSideToAim()
        {
            var directionToEnemy = enemy.position - transform.position;
            var aimDirection = transform.position - origin.position;

            var angleToRotate = Vector3.SignedAngle(aimDirection, directionToEnemy, Vector3.forward);

            Debug.Log($"angleToRotate => {angleToRotate}");

            if (Mathf.Abs(angleToRotate) > 1)
            {
                var sideToAim = (int) Mathf.Sign(angleToRotate);

                inputSystem.AimInputDirection = sideToAim;

                isAimed = false;
                
                return;
            }

            print("aimed");
            isAimed = true;
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            
            Gizmos.color = Color.cyan;
            
            if (enemy != null)
                Gizmos.DrawLine(transform.position, enemy.transform.position);
        }
    }
}