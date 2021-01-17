namespace Planetarity
{
    public class Player : Character
    {
        protected override void TryDie()
        {
            base.TryDie();
            
            level.Fail();
        }
    }
}