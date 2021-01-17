namespace Planetarity
{
    using Input;
    using UnityEngine;

    public class EnemyBattleLogic : MonoBehaviour
    {
        private Enemy enemy;
        private CharacterInputSystem inputSystem;
        private CharacterShootingSystem characterShootingSystem;

        private void Awake()
        {
            inputSystem = GetComponent<CharacterInputSystem>();
            enemy = GetComponent<Enemy>();
        }

        private void Update()
        {
            // // todo: переделать на обсервер
            if (enemy.enemy == null)
            {
                inputSystem.CancelAimInput(default);
                return;
            }

            inputSystem.HandleAimInput(default);
            inputSystem.HandleShootInput(default);
        }
    }
}