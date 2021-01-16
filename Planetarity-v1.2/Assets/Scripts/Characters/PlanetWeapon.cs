using UnityEngine;

namespace Planetarity
{
    using Input;

    public class PlanetWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject rocketPrefab;
        [SerializeField] private float cooldown;

        private float lastShootTime;
        
        private CharacterInputSystem _inputSystem;
        private PlayerAim _playerAim;

        private void Awake()
        {
            _inputSystem = GetComponent<CharacterInputSystem>();
            _playerAim = GetComponent<PlayerAim>();

            _inputSystem.shootEventHandler += Shoot;
        }

        private void Shoot()
        {
            if (!IsCooldownFinished()) return;

            var newRocket = Instantiate(rocketPrefab, _playerAim.aimOrigin.position, _playerAim.aimOrigin.rotation)
                .GetComponent<Rocket>();
            newRocket.parent = gameObject;

            lastShootTime = Time.time;
        }

        private bool IsCooldownFinished() => Time.time - lastShootTime > cooldown;

        private void OnDestroy()
        {
            if (_inputSystem != null)
            {
                _inputSystem.shootEventHandler -= Shoot;
            }
        }
    }
}