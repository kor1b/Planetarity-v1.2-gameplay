namespace Planetarity
{
    using UnityEngine;
    using Input;

    public class CharacterShootingSystem : MonoBehaviour
    {
        [SerializeField] private Missile weapon;

        private float lastShootTime;

        private CharacterInputSystem inputSystem;
        private CharacterAim characterAim;

        private void Awake()
        {
            inputSystem = GetComponent<CharacterInputSystem>();
            characterAim = GetComponent<CharacterAim>();

            inputSystem.OnInputShoot += Shoot;
        }

        public void Construct(Missile weapon)
        {
            this.weapon = weapon;
        }

        private void Shoot()
        {
            if (!IsCooldownFinished()) return;

            var newMissile = Instantiate(weapon, characterAim.origin.position, characterAim.origin.rotation);
            newMissile.parent = gameObject;

            lastShootTime = Time.time;
        }

        private bool IsCooldownFinished() => Time.time - lastShootTime > weapon.cooldown;

        private void OnDestroy()
        {
            if (inputSystem != null)
            {
                inputSystem.OnInputShoot -= Shoot;
            }
        }
    }
}