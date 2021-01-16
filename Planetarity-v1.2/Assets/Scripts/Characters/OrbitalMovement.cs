namespace Planetarity
{
    using UnityEngine;

    public class OrbitalMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 90;

        [SerializeField] private float radius = 2;

        private float _currentAngle;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _currentAngle += speed * Mathf.Deg2Rad * Time.deltaTime;

            var x = Mathf.Cos(_currentAngle);
            var y = Mathf.Sin(_currentAngle);

            transform.position = new Vector2(x, y) * radius;
        }
    }
}