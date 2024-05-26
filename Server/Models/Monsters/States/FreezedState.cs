namespace Server.Models.Monsters.States
{
    public class FreezeState : State
    {
        public override bool CanAttack()
        {
           return false;
        }
    }
}
