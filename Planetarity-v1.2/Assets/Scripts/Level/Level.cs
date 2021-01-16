namespace Planetarity.Level
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Level : MonoBehaviour
    {
        private Player player;
        private List<EnemyAim> enemies;

        private void Awake()
        {
            player = FindObjectOfType<Player>();
            enemies = new List<EnemyAim>(FindObjectsOfType<EnemyAim>());
            
            SetEnemyForCharacters();
        }

        private void SetEnemyForCharacters()
        {
            foreach (var enemy in enemies)
            {
                enemy.enemy = player.transform;
            }
        }
    }
}