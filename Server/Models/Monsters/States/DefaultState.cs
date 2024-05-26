namespace Server.Models.Monsters.States
{
    public class DefaultState : State
    {
        public override bool CanAttack()
        {
            return true;
        }
    }
}
