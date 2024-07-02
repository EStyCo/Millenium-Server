namespace Server.Models.Monsters.States
{
    public class FreezeState : State
    {
        public override bool IsStoppingState { get; } = true;
        public override string Name { get; } = string.Empty;
        public override string Description { get; } = string.Empty;
        public override string ImagePath { get; } = string.Empty;

        public override int CurrentTime { get; set; } = 0;

        public override int MaxTime { get; set; } = 15;

        public FreezeState(Entity user, Entity entity, CancellationTokenSource _CTS) : base(user, entity, _CTS)
        {
            Name = "Заморозка";
            Description = "Замороженная цель не может двигаться и атаковать.";
            ImagePath = "freezing.png";
        }

        public override async Task Enter()
        {
            Refresh();
            Entity.CanAttack = false;

            await Task.Run(async () =>
            {
                while (CurrentTime > 0)
                {
                    await Task.Delay(1000);
                    CurrentTime -= 1;
                }
            }, CTS.Token);

            Entity.CanAttack = true;
            var user = User as ActiveUser;
            user?.AddBattleLog($"{Entity.Name} разморозился!");
            Entity.RemoveState<FreezeState>();
            Entity.UpdateStates();
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
