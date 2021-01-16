namespace Planetarity
{
    using System.Collections;
    using Input;
    using UnityEngine;

    public class CharacterAim : MonoBehaviour
    {
        public Transform aimOrigin;

        [SerializeField] private float rotationSpeed;

        private CharacterInputSystem inputSystem;
        private SphereCollider planetCollider;

        private void Awake()
        {
            planetCollider = GetComponent<SphereCollider>();
            inputSystem = GetComponent<CharacterInputSystem>();

            inputSystem.AimEventHandler += Aim;
        }

        private void Start()
        {
            Vector3 offset = new Vector2(0, planetCollider.radius);
            aimOrigin.localPosition = offset;
        }

        private void Aim()
        {
            StopAllCoroutines();
            StartCoroutine(AimRoutine());
        }

        private IEnumerator AimRoutine()
        {
            while (inputSystem.AimInputDirection != 0)
            {
                aimOrigin.RotateAround(transform.position,
                    aimOrigin.forward,
                    inputSystem.AimInputDirection * -rotationSpeed * Time.deltaTime);

                yield return null;
            }
        }

        private void OnDestroy()
        {
            if (inputSystem != null)
            {
                inputSystem.AimEventHandler -= Aim;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(aimOrigin.position, .05f);
            Gizmos.DrawLine(aimOrigin.position, aimOrigin.position + aimOrigin.up);
        }
    }
}