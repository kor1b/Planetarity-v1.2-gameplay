namespace Planetarity
{
    using UnityEngine;

    [RequireComponent(typeof(EnemyBattleLogic))]
    public class EnemyAim : CharacterAim
    {
        private EnemyBattleLogic enemyBattleLogic;

        private Transform enemy;

        protected override void Awake()
        {
            base.Awake();

            enemyBattleLogic = GetComponent<EnemyBattleLogic>();
        }

        protected override void Start()
        {
            base.Start();

            enemy = enemyBattleLogic.enemy;
        }

        protected override void CalculateSideToAim()
        {
            var directionToEnemy = enemy.position - transform.position;
            var aimDirection = transform.position - origin.position;

            var angleToRotate = Vector3.SignedAngle(aimDirection, directionToEnemy, Vector3.forward);

            while (Mathf.Abs(angleToRotate) > 10)
            {
                var sideToAim = (int) Mathf.Sign(angleToRotate);

                inputSystem.AimInputDirection = sideToAim;

                return;
            }
        }
    }
}