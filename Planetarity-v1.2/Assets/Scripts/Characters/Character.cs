namespace Planetarity
{
    using System;
    using UnityEngine;

    public abstract class Character : MonoBehaviour, IDamageable
    {
        [SerializeField] private float health;

        public void SetScale(float scale)
        {
            transform.localScale = Vector3.one * scale;
        }

        public void TakeDamage(float value)
        {
            health -= value;
            TryDie();
        }

        private void TryDie()
        {
            if (health > 0) return;

            Debug.Log("die");
            Destroy(gameObject);
        }
    }
}