using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planetarity
{
    public class OrbitalMovement : MonoBehaviour
    {
        [SerializeField] private Transform target;

        [SerializeField] private float speed;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.RotateAround(target.transform.position, Vector3.forward, speed * Time.deltaTime);
        }
    }
}