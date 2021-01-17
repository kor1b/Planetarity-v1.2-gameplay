namespace Planetarity
{
    using System;
    using UnityEngine;

    public class OrbitalMovement : MonoBehaviour
    {
        [SerializeField] private OrbitData orbitData;

        private float currentAngle;

        public void Construct(OrbitData data)
        {
            orbitData.speed = data.speed;
            orbitData.radius = data.radius;
            currentAngle = data.startAngle;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            currentAngle += orbitData.speed * Mathf.Deg2Rad * Time.deltaTime;

            var x = Mathf.Cos(currentAngle);
            var y = Mathf.Sin(currentAngle);

            transform.position = new Vector2(x, y) * orbitData.radius;
        }
    }

    [Serializable]
    public struct OrbitData
    {
        public float speed;

        public float radius;

        public float startAngle;
    }
}