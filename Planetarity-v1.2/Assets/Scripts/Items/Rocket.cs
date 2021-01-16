namespace Planetarity
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class Rocket : MonoBehaviour
    {
        [SerializeField] private float speed = 4;
        [SerializeField] private float lifeTime = 10;
        [SerializeField] private float damage;
        
        [HideInInspector] public GameObject parent;
        
        private Rigidbody _rb;
        private TriggerSource _triggerSource;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();

            SubscribeOnTriggerEvents();
            
            DestroyByLifetime(lifeTime);
        }

        private void SubscribeOnTriggerEvents()
        {
            _triggerSource = GetComponent<TriggerSource>();
            _triggerSource.OnEnter += OnEnter;
        }

        private void OnEnter(Collider other)
        {
            if (other.gameObject == parent) return;

            var target = other.GetComponent<IDamageable>();

            target?.TakeDamage(damage);

            Die();
            
            Debug.Log("trigger");
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
            _rb.MovePosition(transform.position + transform.up * (speed * Time.fixedDeltaTime));
        }

        private void DestroyByLifetime(float time)
        {
            Destroy(gameObject, time);
        }
    }
}