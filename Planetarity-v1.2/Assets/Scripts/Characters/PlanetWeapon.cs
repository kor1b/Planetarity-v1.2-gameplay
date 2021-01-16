using UnityEngine;

namespace Planetarity
{
    using Input;

    public class PlanetWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject rocketPrefab;
        
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
            var newRocket = Instantiate(rocketPrefab, _playerAim.aimOrigin.position, _playerAim.aimOrigin.rotation).GetComponent<Rocket>();
            newRocket.parent = gameObject;
        }
        
        private void OnDestroy()
        {
            if (_inputSystem != null)
            {
                _inputSystem.shootEventHandler -= Shoot;
            }
        }
    }
}