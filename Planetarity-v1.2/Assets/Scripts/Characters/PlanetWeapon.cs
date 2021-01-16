using UnityEngine;

namespace Planetarity
{
    using Input;

    public class PlanetWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject rocketPrefab;
        [SerializeField] private float cooldown;

        private float lastShootTime;
        
        private CharacterInputSystem inputSystem;
        private CharacterAim characterAim;

        private void Awake()
        {
            inputSystem = GetComponent<CharacterInputSystem>();
            characterAim = GetComponent<CharacterAim>();

            inputSystem.ShootEventHandler += Shoot;
        }

        private void Shoot()
        {
            if (!IsCooldownFinished()) return;

            var newRocket = Instantiate(rocketPrefab, characterAim.origin.position, characterAim.origin.rotation)
                .GetComponent<Rocket>();
            newRocket.parent = gameObject;

            lastShootTime = Time.time;
        }

        private bool IsCooldownFinished() => Time.time - lastShootTime > cooldown;

        private void OnDestroy()
        {
            if (inputSystem != null)
            {
                inputSystem.ShootEventHandler -= Shoot;
            }
        }
    }
}