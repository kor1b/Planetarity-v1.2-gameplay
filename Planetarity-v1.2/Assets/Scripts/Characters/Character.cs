namespace Planetarity
{
    using System;
    using UnityEngine;
    using LevelBased;

    public abstract class Character : MonoBehaviour, IDamageable
    {
        [SerializeField] protected float health;

        private OrbitalMovement orbitalMovement;
        private CharacterShootingSystem shootingSystem;
        
        protected LevelInstaller levelInstaller;

        private void Awake()
        {
            levelInstaller = FindObjectOfType<LevelInstaller>();

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
            Destroy(gameObject);
        }
    }
}