namespace Planetarity
{
    using UnityEngine;

    [RequireComponent(typeof(EnemyBattleLogic))]
    public class EnemyAim : CharacterAim
    {
        private Enemy thisEnemy;
        private Transform target;

        protected override void Awake()
        {
            base.Awake();
            
            thisEnemy = GetComponent<Enemy>();
        }

        protected override void Start()
        {
            base.Start();

            target = thisEnemy.enemy;
        }

        protected override void CalculateSideToAim()
        {
            var directionToEnemy = target.position - transform.position;
            var aimDirection = transform.position - origin.position;

            var angleToRotate = Vector3.SignedAngle(aimDirection, directionToEnemy, Vector3.forward);

            if (Mathf.Abs(angleToRotate) > 1)
            {
                var sideToAim = (int) Mathf.Sign(angleToRotate);

                inputSystem.AimInputDirection = sideToAim;
            }
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            
            Gizmos.color = Color.cyan;
            
            if (target != null)
                Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }
}