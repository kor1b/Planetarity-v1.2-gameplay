namespace Planetarity
{
    using Input;
    using UnityEngine;

    public class EnemyBattleLogic : MonoBehaviour
    {
        private CharacterInputSystem inputSystem;
        private EnemyAim aim;
        private CharacterShooting characterShooting;

        private void Awake()
        {
            inputSystem = GetComponent<CharacterInputSystem>();
            aim = GetComponent<EnemyAim>();
        }

        private void Update()
        {
            // // todo: переделать на обсервер
            if (aim.enemy == null)
            {
                inputSystem.CancelAimInput(default);
                return;
            }

            inputSystem.HandleAimInput(default);
            inputSystem.HandleShootInput(default);
        }
    }
}