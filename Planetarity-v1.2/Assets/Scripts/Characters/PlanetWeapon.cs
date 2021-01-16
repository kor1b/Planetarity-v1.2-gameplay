using UnityEngine;

namespace Planetarity
{
    public class PlanetWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject rocketPrefab;
        
        private PlayerInputSystem _playerInputSystem;
        private PlayerAim _playerAim;

        private void Awake()
        {
            _playerInputSystem = GetComponent<PlayerInputSystem>();
            _playerAim = GetComponent<PlayerAim>();

            _playerInputSystem.shootEventHandler += Shoot;
        }
        
        private void Shoot()
        {
            var newRocket = Instantiate(rocketPrefab, _playerAim.aimOrigin.position, _playerAim.aimOrigin.rotation).GetComponent<Rocket>();
            newRocket.parent = gameObject;
        }
        
        private void OnDestroy()
        {
            if (_playerInputSystem != null)
            {
                _playerInputSystem.shootEventHandler -= Shoot;
            }
        }
    }
}