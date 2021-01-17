namespace Planetarity
{
    public class Enemy : Character
    {
        protected override void TryDie()
        {
            base.TryDie();

            level.enemies.Remove(this);
            level.CheckWin();
        }
    }
}