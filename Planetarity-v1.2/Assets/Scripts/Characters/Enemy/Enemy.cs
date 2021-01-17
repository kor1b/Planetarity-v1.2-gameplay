namespace Planetarity
{
    using UnityEngine;

    public class Enemy : Character
    {
       [HideInInspector] public Transform enemy;    // The enemy of this character

        protected override void TryDie()
        {
            if (health > 0) return;
            
            levelInstaller.enemies.Remove(this);
            levelInstaller.levelResultComparer.CheckWin();
            
            base.TryDie();
        }
    }
}