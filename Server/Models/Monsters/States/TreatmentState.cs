
using Server.Models.EntityFramework;
using Server.Models.Monsters.States;
using Server.Models;

namespace Server.Models.Monsters.States
{
    public class TreatmentState : State
    {
        public override string Name { get; } = string.Empty;
        public override string Description { get; } = string.Empty;
        public override string ImagePath { get; } = string.Empty;
        public override int CurrentTime { get; set; } = 0;
        public override int MaxTime { get; set; } = 20;

        public TreatmentState(Entity user, Entity entity, CancellationTokenSource _CTS) : base(user, entity, _CTS)
        {
            Name = "Аура восстановления";
            Description = "Постепенно восстанавливает здоровье";
            ImagePath = "treatment.png";
        }

        public override async Task Enter()
        {
            Refresh();
            var user = User as ActiveUser;
            int damage = (int)(Entity.Vitality.MaxHP / 100.0 * 7);

            int healing = (int)(Entity.Vitality.MaxHP / 100.0 * 3);
            await Task.Run(async () =>
            {
                while (CurrentTime > 0)
                {
                    await Task.Delay(1000);
                    CurrentTime -= 1;

                    Entity.TakeHealing(healing);
                    _ = user?.AddBattleLog($"{Entity.Name} восстановил {healing} здоровья.");
                }
            }, CTS.Token);

            user?.AddBattleLog($"У {Entity.Name} закончилась аура восстановления.");
            Entity.RemoveState<TreatmentState>();
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