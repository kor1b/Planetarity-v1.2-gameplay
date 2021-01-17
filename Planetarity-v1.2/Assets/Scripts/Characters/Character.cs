namespace Planetarity
{
    using System;
    using UnityEngine;
    using LevelBased;

    public abstract class Character : MonoBehaviour, IDamageable
    {
        [SerializeField] private float health;

        private OrbitalMovement orbitalMovement;
        private CharacterShootingSystem shootingSystem;
        
        protected Level level;

        private void Awake()
        {
            level = FindObjectOfType<Level>();

            orbitalMovement = GetComponent<OrbitalMovement>();
            shootingSystem = GetComponent<CharacterShootingSystem>();
        }

        public void SetScale(float scale)
        {
            transform.localScale = Vector3.one * scale;
        }
        
        public void SetOrbitData(OrbitData data)
        {
            orbitalMovement.Construct(data);
        }

        public void SetWeapon(Rocket weapon)
        {
            shootingSystem.Construct(weapon);
        }

        public void TakeDamage(float value)
        {
            health -= value;
            TryDie();
        }

        protected virtual void TryDie()
        {
            if (health > 0) return;

            Destroy(gameObject);
        }
    }
}