namespace Planetarity
{
    using System;
    using System.Collections;
    using Input;
    using UnityEngine;

    public abstract class CharacterAim : MonoBehaviour
    {
        public Transform origin;

        [SerializeField] private float rotationSpeed = 90;

        protected CharacterInputSystem inputSystem;
        private SphereCollider planetCollider;

        protected virtual void Awake()
        {
            planetCollider = GetComponent<SphereCollider>();

            inputSystem = GetComponent<CharacterInputSystem>();
            inputSystem.OnInputAim += StartAim;
        }

        protected virtual void Start()
        {
            Vector3 offset = new Vector2(0, planetCollider.radius);
            origin.localPosition = offset;
        }

        private void StartAim()
        {
            StopAllCoroutines();
            CalculateSideToAim();
            StartCoroutine(RotateAimRoutine());
        }

        protected abstract void CalculateSideToAim();

        private IEnumerator RotateAimRoutine()
        {
            while (inputSystem.AimInputDirection != 0)
            {
                origin.RotateAround(transform.position,
                    origin.forward,
                    inputSystem.AimInputDirection * -rotationSpeed * Time.deltaTime);

                yield return null;
            }
        }

        private void OnDestroy()
        {
            if (inputSystem != null)
            {
                inputSystem.OnInputAim -= StartAim;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(origin.position, .05f);
            Gizmos.DrawLine(origin.position, origin.position + origin.up);
        }
    }
}