namespace Planetarity
{
    using UnityEngine;
    using Input;

    public abstract class CharacterShooting : MonoBehaviour
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

            inputSystem.OnInputShoot += Shoot;
        }

        private void Shoot()
        {
            if (!CanShoot()) return;

            var newRocket = Instantiate(rocketPrefab, characterAim.origin.position, characterAim.origin.rotation)
                .GetComponent<Rocket>();
            newRocket.parent = gameObject;

            lastShootTime = Time.time;
        }

        protected virtual bool CanShoot() => IsCooldownFinished();

        private bool IsCooldownFinished() => Time.time - lastShootTime > cooldown;

        private void OnDestroy()
        {
            if (inputSystem != null)
            {
                inputSystem.OnInputShoot -= Shoot;
            }
        }
    }
}