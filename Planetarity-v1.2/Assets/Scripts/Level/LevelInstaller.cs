namespace Planetarity.LevelBased
{
    using System.Collections.Generic;
    using UnityEngine;

    public class LevelInstaller : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject enemyPrefab;

        [Min(1), SerializeField] private int enemiesAmount;

        [SerializeField] private float minPlanetScale;
        [SerializeField] private float maxPlanetScale;

        [Header("Health")] [SerializeField] private float minEnemyHealth;
        [SerializeField] private float maxEnemyHealth;

        [SerializeField] private float playerHealth;

        [Header("Orbits")] [SerializeField] private float minOrbitSpeed;
        [SerializeField] private float maxOrbitSpeed;

        [SerializeField] private float minOrbitRadius;
        [SerializeField] private float distanceBtwOrbits;

        [SerializeField] private float minStartAngle;
        [SerializeField] private float maxStartAngle;

        [HideInInspector] public Player player;
        [HideInInspector] public List<Enemy> enemies;

        [Header("Weapon")] [SerializeField] private Missile[] allowedWeapon;

        private int playerOrderPosition;

        private float tempPlayerSpawnRadius;
        private float spawnRadius;

        [HideInInspector] public LevelResultComparer levelResultComparer;

        private void Awake()
        {
            levelResultComparer = FindObjectOfType<LevelResultComparer>();

            SpawnCharacters();
        }

        private void SpawnCharacters()
        {
            playerOrderPosition = Random.Range(0, enemiesAmount);
            enemies = new List<Enemy>(enemiesAmount);

            SpawnEnemies();

            spawnRadius = tempPlayerSpawnRadius;
            player = (Player) SpawnRandomCharacter(playerPrefab);

            SetEnemyForCharacters(player);
        }

        private void SpawnEnemies()
        {
            var lastSpawnRadius = minOrbitRadius;

            for (int i = 0; i < enemiesAmount; i++)
            {
                if (i == playerOrderPosition)
                {
                    tempPlayerSpawnRadius = lastSpawnRadius;
                    lastSpawnRadius += distanceBtwOrbits;
                }

                spawnRadius = lastSpawnRadius;
                var newEnemy = SpawnRandomCharacter(enemyPrefab);

                enemies.Add(newEnemy.GetComponent<Enemy>());

                lastSpawnRadius += distanceBtwOrbits;
            }
        }

        private Character SpawnRandomCharacter(GameObject prefab)
        {
            var newCharacter = Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<Character>();

            SetData(newCharacter);

            return newCharacter;
        }

        private void SetData(Character character)
        {
            SetRandomOrbit(character);
            SetRandomScale(character);
            SetRandomWeapon(character);
            SetHealth(character);
        }

        private void SetRandomOrbit(Character character)
        {
            var randomData = new OrbitData()
            {
                radius = spawnRadius,
                speed = Random.Range(minOrbitSpeed, maxOrbitSpeed),
                startAngle = Random.Range(minStartAngle, maxStartAngle) * Mathf.Deg2Rad
            };

            character.SetOrbitData(randomData);
        }

        private void SetRandomScale(Character character)
        {
            var randomScale = Random.Range(minPlanetScale, maxPlanetScale);
            character.SetScale(randomScale);
        }

        private void SetRandomWeapon(Character character)
        {
            var randomWeapon = Random.Range(0, allowedWeapon.Length);
            character.SetWeapon(allowedWeapon[randomWeapon]);
        }

        private void SetHealth(Character character)
        {
            var health = 0f;

            switch (character)
            {
                case Enemy _:
                    health = Random.Range(minEnemyHealth, maxEnemyHealth);
                    break;
                case Player _:
                    health = playerHealth;
                    break;
            }

            character.SetHealth(health);
        }

        private void SetEnemyForCharacters(Character enemyTarget)
        {
            foreach (var enemy in enemies)
            {
                enemy.enemy = enemyTarget.transform;
            }
        }
    }
}