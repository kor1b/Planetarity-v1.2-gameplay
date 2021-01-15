using UnityEngine;

namespace Planetarity
{
    public class PlanetWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject rocket;
        
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
            Instantiate(rocket, _playerAim.aimOrigin.position, Quaternion.identity);
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