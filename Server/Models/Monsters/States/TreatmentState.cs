
namespace Server.Models.Monsters.States
{
    public class TreatmentState : State
    {
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

        public override async void Enter()
        {
            var user = User as ActiveUser;

            _ = user?.AddBattleLog($"{Entity.Name} использовал Ауру восстановления.");
            _ = Task.Delay(20000, CTS.Token).ContinueWith(_ =>
            {
                Entity.RemoveState<TreatmentState>();
            });

            int healing = (int)(Entity.Vitality.MaxHP / 100.0 * 3);

            await Task.Run(async () =>
            {
                while (!CTS.IsCancellationRequested)
                {
                    await Task.Delay(1000);
                    Entity.TakeHealing(healing);
                    _ = user?.AddBattleLog($"{Entity.Name} восстановил {healing} здоровья.");
                }
                Entity.RemoveState<TreatmentState>();
            }, CTS.Token);

            _ = user?.AddBattleLog($"У {Entity.Name} закончилось Аура восстановления");
            user?.UpdateStates();
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }
    }
}
