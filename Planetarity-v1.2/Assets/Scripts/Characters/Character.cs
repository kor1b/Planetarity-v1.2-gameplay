namespace Planetarity
{
    using UnityEngine;

    public abstract class Character : MonoBehaviour, IDamageable
    {
        [SerializeField] private float health;

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