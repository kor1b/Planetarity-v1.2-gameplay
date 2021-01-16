namespace Planetarity
{
    using UnityEngine;

    [RequireComponent(typeof(EnemyBattleLogic))]
    public class EnemyAim : CharacterAim
    {
        public Transform enemy;

        protected override void CalculateSideToAim()
        {
            var directionToEnemy = enemy.position - transform.position;
            var aimDirection = transform.position - origin.position;

            var angleToRotate = Vector3.SignedAngle(aimDirection, directionToEnemy, Vector3.forward);

            while (Mathf.Abs(angleToRotate) > 0)
            {
                var sideToAim = (int) Mathf.Sign(angleToRotate);

                inputSystem.AimInputDirection = sideToAim;

                return;
            }
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