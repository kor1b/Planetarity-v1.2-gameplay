﻿namespace Planetarity
{
    using UnityEngine;

    public class OrbitalMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 90;

        public float radius = 2;

        private float currentAngle;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            currentAngle += speed * Mathf.Deg2Rad * Time.deltaTime;

            var x = Mathf.Cos(currentAngle);
            var y = Mathf.Sin(currentAngle);

            transform.position = new Vector2(x, y) * radius;
        }
    }
}