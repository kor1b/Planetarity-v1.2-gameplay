namespace Planetarity.Level
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class Level : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject enemyPrefab;

        [Min(1), SerializeField] private int enemiesAmount;

        [SerializeField] private float minPlanetSize;
        [SerializeField] private float maxPlanetSize;

        [SerializeField] private float minOrbitRadius;
        [SerializeField] private float distanceBtwOrbits;

        private Player player;
        private List<EnemyAim> enemies;

        private int playerOrderPosition;
        private float lastOrbitRadius;

        private float playerSpawnRadius;

        private bool isEnd = false;

        private void Start()
        {
            // todo: учитывать размер планеты для дельты между орбитами 

            lastOrbitRadius = minOrbitRadius;

            playerOrderPosition = Random.Range(0, enemiesAmount);
            enemies = new List<EnemyAim>(enemiesAmount);

            for (int i = 0; i < enemiesAmount; i++)
            {
                if (i == playerOrderPosition)
                {
                    playerSpawnRadius = lastOrbitRadius;
                    lastOrbitRadius += distanceBtwOrbits;
                }

                var newEnemy = Instantiate(enemyPrefab, new Vector3(0, lastOrbitRadius, 0), Quaternion.identity)
                    .GetComponent<OrbitalMovement>();

                newEnemy.radius = lastOrbitRadius;

                enemies.Add(newEnemy.GetComponent<EnemyAim>());


                lastOrbitRadius += distanceBtwOrbits;
            }

            Debug.Log($"playerSpawnRadius => {playerSpawnRadius}");

            player = Instantiate(playerPrefab, new Vector3(0, playerSpawnRadius, 0), Quaternion.identity)
                .GetComponent<Player>();
            player.GetComponent<OrbitalMovement>().radius = playerSpawnRadius;

            SetEnemyForCharacters();
        }

        private void SetEnemyForCharacters()
        {
            foreach (var enemy in enemies)
            {
                enemy.enemy = player.transform;
            }
        }

        public void CheckWin()
        {
            if (enemies.Count <= 0)
            {
                print("Win");
            }
        }

        public void Fail()
        {
            if (isEnd) return;

            isEnd = true;
            print("Fail");
        }
    }
}