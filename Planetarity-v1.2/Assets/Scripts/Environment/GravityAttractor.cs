namespace Planetarity
{
    using UnityEngine;

    public class GravityAttractor : MonoBehaviour
    {
        private const float G = 667.4f; // Gravitational constant

        [Min(0), SerializeField] private float mass = 1;
        [SerializeField] private float radius;

        private void FixedUpdate()
        {
            GravityFieldTick();
        }

        private void GravityFieldTick()
        {
            var targets = Physics.OverlapSphere(transform.position, transform.localScale.x + radius);

            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].TryGetComponent(out GravityAttractive target))
                    Attract(target);
            }
        }

        private void Attract(GravityAttractive objToAttract)
        {
            if (IsTryAttractOwnRocket(objToAttract)) return;
            
            var direction = transform.position - objToAttract.transform.position;
            var distance = direction.magnitude;

            var forceMagnitude = G * (mass * objToAttract.mass) / Mathf.Pow(distance, 2);
            var force = direction.normalized * forceMagnitude;

            var rbToAttract = objToAttract.GetComponent<Rigidbody>();
            rbToAttract.AddForce(force);
        }

        private bool IsTryAttractOwnRocket(GravityAttractive objToAttract)
        {
            return objToAttract.TryGetComponent(out Rocket rocket) && rocket.parent == gameObject;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}