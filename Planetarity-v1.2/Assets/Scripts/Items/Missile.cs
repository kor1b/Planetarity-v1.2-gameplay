namespace Planetarity
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class Missile : MonoBehaviour
    {
        [SerializeField] private float speed = 4;
        [SerializeField] private float lifeTime = 10;
        [SerializeField] private float damage;

        public float cooldown;

        [HideInInspector] public GameObject parent;
        
        private Rigidbody rb;
        private TriggerSource triggerSource;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

            SubscribeOnTriggerEvents();
            
            DestroyByLifetime(lifeTime);
        }

        private void SubscribeOnTriggerEvents()
        {
            triggerSource = GetComponent<TriggerSource>();
            triggerSource.OnEnter += OnEnter;
        }

        private void OnEnter(Collider other)
        {
            if (other.gameObject == parent) return;

            var target = other.GetComponent<IDamageable>();

            target?.TakeDamage(damage);

            Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            rb.MovePosition(transform.position + transform.up * (speed * Time.fixedDeltaTime));
        }

        private void DestroyByLifetime(float time)
        {
            Destroy(gameObject, time);
        }
    }
}