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

        public Player player;
        public List<Enemy> enemies;

        private int playerOrderPosition;
        private float lastOrbitRadius;

        private float playerSpawnRadius;

        private bool isEnd = false;

        private void Start()
        {
            // todo: учитывать размер планеты для дельты между орбитами 

            lastOrbitRadius = minOrbitRadius;

            playerOrderPosition = Random.Range(0, enemiesAmount);
            enemies = new List<Enemy>(enemiesAmount);

            for (int i = 0; i < enemiesAmount; i++)
            {
                if (i == playerOrderPosition)
                {
                    playerSpawnRadius = lastOrbitRadius;
                    lastOrbitRadius += distanceBtwOrbits;
                }

                var newEnemy = Instantiate(enemyPrefab, new Vector3(0, lastOrbitRadius, 0), Quaternion.identity);

                OrbitData data = new OrbitData()
                {
                    radius = lastOrbitRadius,
                    speed = 20,
                    startAngle = 0
                };
                newEnemy.GetComponent<Enemy>().SetOrbitData(data);
                // newEnemy.radius = lastOrbitRadius;

                enemies.Add(newEnemy.GetComponent<Enemy>());


                lastOrbitRadius += distanceBtwOrbits;
            }

            Debug.Log($"playerSpawnRadius => {playerSpawnRadius}");

            player = Instantiate(playerPrefab, new Vector3(0, playerSpawnRadius, 0), Quaternion.identity)
                .GetComponent<Player>();
            
            OrbitData data2 = new OrbitData()
            {
                radius = playerSpawnRadius,
                speed = 20,
                startAngle = 0
            };
            
            player.GetComponent<Player>().SetOrbitData(data2);

            
            // player.GetComponent<OrbitalMovement>().radius = playerSpawnRadius;

            SetEnemyForCharacters();
        }

        private void SpawnCharacter()
        {
            
        }

        private void SetRandomOrbit(Character character, OrbitData data)
        {
            character.SetOrbitData(data);
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