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

        [Header("Orbits")]
        
        [SerializeField] private float minOrbitSpeed;
        [SerializeField] private float maxOrbitSpeed;
        
        [SerializeField] private float minOrbitRadius;
        [SerializeField] private float distanceBtwOrbits;

        public Player player;
        public List<Enemy> enemies;

        private int playerOrderPosition;
        private float playerSpawnRadius;

        private bool isEnd = false;

        private void Start()
        {
            SpawnCharacters();
        }

        private void SpawnCharacters()
        {
            playerOrderPosition = Random.Range(0, enemiesAmount);
            enemies = new List<Enemy>(enemiesAmount);
            
            SpawnEnemies();
            
            player = (Player) SpawnRandomCharacter(playerPrefab, playerSpawnRadius);
            
            SetEnemyForCharacters(player);
        }

        private void SpawnEnemies()
        {
            var lastOrbitRadius = minOrbitRadius;
            
            for (int i = 0; i < enemiesAmount; i++)
            {
                if (i == playerOrderPosition)
                {
                    playerSpawnRadius = lastOrbitRadius;
                    lastOrbitRadius += distanceBtwOrbits;
                }

                var newEnemy = SpawnRandomCharacter(enemyPrefab, lastOrbitRadius);
               
                enemies.Add(newEnemy.GetComponent<Enemy>());

                lastOrbitRadius += distanceBtwOrbits;
            }
        }

        private Character SpawnRandomCharacter(GameObject prefab, float spawnRadius)
        {
            var newCharacter = Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<Character>();
            
            var data = new OrbitData()
            {
                radius = spawnRadius,
                speed = Random.Range(minOrbitSpeed, maxOrbitSpeed),
                startAngle = 0
            };
            
            newCharacter.SetOrbitData(data);

            return newCharacter;
        }

        private void SetEnemyForCharacters(Character enemyTarget)
        {
            foreach (var enemy in enemies)
            {
                enemy.enemy = enemyTarget.transform;
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