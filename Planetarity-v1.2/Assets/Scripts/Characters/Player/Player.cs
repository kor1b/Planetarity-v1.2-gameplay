namespace Planetarity
{
    public class Player : Character
    {
        protected override void TryDie()
        {
            if (health > 0) return;

            levelInstaller.levelResultComparer.Fail();

            base.TryDie();
        }
    }
}