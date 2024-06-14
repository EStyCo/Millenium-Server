
namespace Server.Models.Monsters.States
{
    public class TreatmentState : State
    {
        public override bool IsStoppingSpell { get; } = false;
        public override string Name { get; } = string.Empty;
        public override string Description { get; } = string.Empty;
        public override string ImagePath { get; } = string.Empty;

        public TreatmentState(Entity user, Entity entity, CancellationTokenSource _CTS) : base(user, entity, _CTS)
        {
            Name = "Аура восстановления";
            Description = "Постепенно восстанавливает здоровье";
            ImagePath = "treatment.png";

            Enter();
        }

        public override void Enter()
        {
            throw new NotImplementedException();
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }
    }
}
