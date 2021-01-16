using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
    [SerializeField] private float speed = 4;
    [SerializeField] private float lifeTime = 10;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        
        DestroyByLifetime(lifeTime);
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
