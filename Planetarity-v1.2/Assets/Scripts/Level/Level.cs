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

        [SerializeField] private float minPlanetScale;
        [SerializeField] private float maxPlanetScale;

        [Header("Orbits")]
        
        [SerializeField] private float minOrbitSpeed;
        [SerializeField] private float maxOrbitSpeed;
        
        [SerializeField] private float minOrbitRadius;
        [SerializeField] private float distanceBtwOrbits;

        [SerializeField] private float minStartAngle;
        [SerializeField] private float maxStartAngle;
        
        [HideInInspector] public Player player;
        [HideInInspector] public List<Enemy> enemies;

        [Header("Weapon")]
        
        [SerializeField] private Rocket[] allowedWeapon;

        private int playerOrderPosition;
        private float playerSpawnRadius;

        private bool isEnd = false;

        private void Awake()
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
            
            var randomData = new OrbitData()
            {
                radius = spawnRadius,
                speed = Random.Range(minOrbitSpeed, maxOrbitSpeed),
                startAngle = Random.Range(minStartAngle, maxStartAngle) * Mathf.Deg2Rad
            };

            var randomScale = Random.Range(minPlanetScale, maxPlanetScale);
            var randomWeapon = Random.Range(0, allowedWeapon.Length);
            
            newCharacter.SetOrbitData(randomData);
            newCharacter.SetScale(randomScale);
            newCharacter.SetWeapon(allowedWeapon[randomWeapon]);
            
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