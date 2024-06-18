
namespace Server.Models.Monsters.States
{
    public class BleedingState : State
    {
        public override string Name { get; } = string.Empty;
        public override string Description { get; } = string.Empty;
        public override string ImagePath { get; } = string.Empty;

        public BleedingState(Entity user, Entity entity, CancellationTokenSource _CTS) : base(user, entity, _CTS)
        {
            Name = "Кровотечение";
            Description = "Наносит постепенный урон";
            ImagePath = "bleeding.png";

            Enter();
        }

        public override async void Enter()
        {
            var user = User as ActiveUser;

            _ = user?.AddBattleLog($"{user.Name} наложил кровотечение на {Entity.Name}.");
            _ = Task.Delay(30000, CTS.Token).ContinueWith(_ =>
            {
                Entity.RemoveState<BleedingState>();
            });

            int damage = (int)(Entity.Vitality.MaxHP / 100.0 * 7);

            await Task.Run(async () =>
            {
                while (Entity.Vitality.CurrentHP > 0)
                {
                    await Task.Delay(3000);
                    Entity.TakeDamage(damage);
                    _ = user?.AddBattleLog($"Противнику {Entity.Name} нанесено {damage} кровотечением.");
                }
                Entity.RemoveState<BleedingState>();
            }, CTS.Token);

            _ = user?.AddBattleLog($"У {Entity.Name} закончилось кровотечение");
        }

        public override void Exit()
        {
            //throw new NotImplementedException();
        }
    }
}