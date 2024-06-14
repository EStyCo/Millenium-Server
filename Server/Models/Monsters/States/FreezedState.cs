using System.Runtime.CompilerServices;

namespace Server.Models.Monsters.States
{
    public class FreezeState : State
    {
        public override bool IsStoppingSpell { get; } = true;
        public override string Name { get; } = string.Empty;
        public override string Description { get; } = string.Empty;
        public override string ImagePath { get; } = string.Empty;

        public FreezeState(Entity user, Entity entity, CancellationTokenSource _CTS) : base(user, entity, _CTS)
        {
            Name = "Заморозка";
            Description = "Замороженная цель не может двигаться и атаковать.";
            ImagePath = "freezing.png";

            Enter();
        }

        public override async void Enter()
        {
            var user = User as ActiveUser;
            try
            {
                Entity.CanAttack = false;
                await Task.Delay(25000, CTS.Token);
                CTS.Cancel();
            }
            catch (OperationCanceledException)
            {
                _ = user?.AddBattleLog($"{Entity.Name} отменил заморозку.");
            }
            finally
            {
                _ = user?.AddBattleLog($"{Entity.Name} разморозился!");
                Entity.RemoveState<FreezeState>();
            }

        }

        public override void Exit()
        {
            //Console.WriteLine($"{Entity.Name} отменил заморозку.");
        }
    }
}
