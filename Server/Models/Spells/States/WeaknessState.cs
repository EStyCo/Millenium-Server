namespace Server.Models.Spells.States
{
    public class WeaknessState : State
    {
        public override string Name { get; } = string.Empty;
        public override string Description { get; } = string.Empty;
        public override string ImagePath { get; } = string.Empty;
        public override int CurrentTime { get; set; } = 0;
        public override int MaxTime { get; set; } = 15;

        public WeaknessState(Entity user, Entity entity, CancellationTokenSource _CTS) : base(user, entity, _CTS)
        {
            Name = "Слабость";
            Description = "Игрок ослаблен и не может сражаться.";
            ImagePath = "spells/weakness.png";
        }

        public override async Task Enter()
        {
            Refresh();
            var user = Entity as ActiveUser;
            if (user == null) return;

            _ = user.AddBattleLog($"{Entity.Leading()} ослаблен /ispells/weakness.png/i и не может дальше сражаться");
            user.CanAttack = false;

            await Task.Run(async () =>
            {
                while (CurrentTime > 0)
                {
                    await Task.Delay(1000);
                    CurrentTime -= 1;
                }
            }, CTS.Token);

            user.CanAttack = true;
            Entity.RemoveState<WeaknessState>();
            _ = user.AddBattleLog($"{Entity.Leading()} восстановил силы! /ispells/unweakness.png/i");
            user.UpdateStates();
        }

        public override void Exit()
        {
            CurrentTime = 0;
            CTS.Cancel();
        }

        public override void Refresh()
        {
            CurrentTime = MaxTime;
        }
    }
}
