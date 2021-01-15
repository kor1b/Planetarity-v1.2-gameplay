namespace Planetarity
{
    using System.Collections;
    using UnityEngine;

    public class PlayerAim : MonoBehaviour
    {
        [SerializeField] private Transform aimOrigin;

        [SerializeField] private float rotationSpeed;

        private PlayerInputSystem _playerInputSystem;
        private SphereCollider _collider;

        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
            _playerInputSystem = GetComponent<PlayerInputSystem>();

            _playerInputSystem.aimInputEventHandler += Aim;
        }

        private void Start()
        {
            Vector3 offset = new Vector2(0, _collider.radius);
            aimOrigin.localPosition = offset;
        }

        private void Aim()
        {
            StopAllCoroutines();
            StartCoroutine(AimRoutine());
        }

        private IEnumerator AimRoutine()
        {
            while (_playerInputSystem.AimInputDirection != 0)
            {
                aimOrigin.RotateAround(transform.position,
                    aimOrigin.forward,
                    _playerInputSystem.AimInputDirection * -rotationSpeed * Time.deltaTime);
                
                yield return null;
            }
        }
        
        private void OnDestroy()
        {
            if (_playerInputSystem != null)
            {
                _playerInputSystem.aimInputEventHandler -= Aim;
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