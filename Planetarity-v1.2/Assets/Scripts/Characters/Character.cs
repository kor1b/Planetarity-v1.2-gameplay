namespace Planetarity
{
    using System;
    using UnityEngine;
    using LevelBased;
    using TMPro;

    public abstract class Character : MonoBehaviour, IDamageable
    {
        [SerializeField] protected float health;

        protected LevelInstaller levelInstaller;
        
        private OrbitalMovement orbitalMovement;
        private CharacterShootingSystem shootingSystem;

        private CharacterHealthView healthView; 
        
        private void Awake()
        {
            levelInstaller = FindObjectOfType<LevelInstaller>();

            orbitalMovement = GetComponent<OrbitalMovement>();
            shootingSystem = GetComponent<CharacterShootingSystem>();
            
            healthView = new CharacterHealthView(GetComponentInChildren<TextMeshProUGUI>());
        }

        public void SetHealth(float value)
        {
            health = value;
            healthView.DisplayHealth(health);
        }

        public void SetScale(float scale)
        {
            transform.localScale = Vector3.one * scale;
        }
        
        public void SetOrbitData(OrbitData data)
        {
            orbitalMovement.Construct(data);
        }

        public void SetWeapon(Missile weapon)
        {
            shootingSystem.Construct(weapon);
        }

        public void TakeDamage(float value)
        {
            health -= value;
            SetHealth(health);
            TryDie();
        }

        protected virtual void TryDie()
        {
            Destroy(gameObject);
        }
    }
}