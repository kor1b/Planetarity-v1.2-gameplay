namespace Planetarity
{
    using System.Collections;
    using Input;
    using UnityEngine;

    public class CharacterAim : MonoBehaviour
    {
        public Transform aimOrigin;

        [SerializeField] private float rotationSpeed;

        private CharacterInputSystem _inputSystem;
        private SphereCollider _planetCollider;

        private void Awake()
        {
            _planetCollider = GetComponent<SphereCollider>();
            _inputSystem = GetComponent<CharacterInputSystem>();

            _inputSystem.aimEventHandler += Aim;
        }

        private void Start()
        {
            Vector3 offset = new Vector2(0, _planetCollider.radius);
            aimOrigin.localPosition = offset;
        }

        private void Aim()
        {
            StopAllCoroutines();
            StartCoroutine(AimRoutine());
        }

        private IEnumerator AimRoutine()
        {
            while (_inputSystem.AimInputDirection != 0)
            {
                aimOrigin.RotateAround(transform.position,
                    aimOrigin.forward,
                    _inputSystem.AimInputDirection * -rotationSpeed * Time.deltaTime);

                yield return null;
            }
        }

        private void OnDestroy()
        {
            if (_inputSystem != null)
            {
                _inputSystem.aimEventHandler -= Aim;
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