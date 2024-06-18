namespace Server.Models.Monsters.States
{
    public class WeaknessState : State
    {
        public override string Name { get; } = string.Empty;
        public override string Description { get; } = string.Empty;
        public override string ImagePath { get; } = string.Empty;

        public WeaknessState(Entity user, Entity entity, CancellationTokenSource _CTS) : base(user, entity, _CTS)
        {
            Name = "Слабость";
            Description = "Игрок ослаблен и не может сражаться.";
            ImagePath = "weakness.png";

            Enter();
        }

        public override async void Enter()
        {
            var user = Entity as ActiveUser;
            if (user == null) return;

            _ = user?.AddBattleLog($"{Entity.Name} ослаблен и не может дальше сражаться");
            user.CanAttack = false;

            await Task.Run(async () =>
            {
                await Task.Delay(20000);
                Entity.RemoveState<WeaknessState>();
            }, CTS.Token);

            user.CanAttack = true;
            _ = user?.AddBattleLog($"{Entity.Name} восстановил силы!");
            user.UpdateStates();
        }

        public override void Exit()
        {
            //throw new NotImplementedException();
        }
    }
}
