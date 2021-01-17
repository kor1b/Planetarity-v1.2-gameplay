namespace Planetarity
{
    using UnityEngine;

    public class Enemy : Character
    {
       [HideInInspector] public Transform enemy;    // The enemy of this character

        protected override void TryDie()
        {
            base.TryDie();

            level.enemies.Remove(this);
            level.CheckWin();
        }
    }
}