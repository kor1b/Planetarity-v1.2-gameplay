namespace Planetarity
{
    using System.Collections;
    using Input;
    using UnityEngine;

    public class CharacterAim : MonoBehaviour
    {
        public Transform origin;

        [SerializeField] private float rotationSpeed;

        private CharacterInputSystem inputSystem;
        private SphereCollider planetCollider;

        private void Awake()
        {
            planetCollider = GetComponent<SphereCollider>();
            inputSystem = GetComponent<CharacterInputSystem>();

            inputSystem.AimEventHandler += StartAim;
        }

        private void Start()
        {
            Vector3 offset = new Vector2(0, planetCollider.radius);
            origin.localPosition = offset;
        }

        private void StartAim()
        {
            StopAllCoroutines();
            StartCoroutine(AimRoutine());
        }

        private IEnumerator AimRoutine()
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
                inputSystem.AimEventHandler -= StartAim;
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