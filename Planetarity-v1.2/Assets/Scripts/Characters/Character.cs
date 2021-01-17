namespace Planetarity
{
    using System;
    using UnityEngine;

    public abstract class Character : MonoBehaviour, IDamageable
    {
        [SerializeField] private float health;

        protected Level.Level level;

        private void Awake()
        {
            level = FindObjectOfType<Level.Level>();
        }

        public void SetScale(float scale)
        {
            transform.localScale = Vector3.one * scale;
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