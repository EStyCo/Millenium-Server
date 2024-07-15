namespace Server.Models.Spells.States
{
    public class BleedingState : State
    {
        public override string Name { get; } = string.Empty;
        public override string Description { get; } = string.Empty;
        public override string ImagePath { get; } = string.Empty;
        public override int CurrentTime { get; set; } = 0;
        public override int MaxTime { get; set; } = 10;

        public BleedingState(Entity user, Entity entity, CancellationTokenSource _CTS) : base(user, entity, _CTS)
        {
            Name = "Кровотечение";
            Description = "Наносит постепенный урон";
            ImagePath = "spells/bleeding.png";
        }

        public override async Task Enter()
        {
            Refresh();
            var user = User as ActiveUser;
            int damage = (int)(Entity.Vitality.MaxHP / 100.0 * 7);

            await Task.Run(async () =>
            {
                while (CurrentTime > 0)
                {
                    await Task.Delay(3000);
                    CurrentTime -= 3;
                    Entity.TakeDamage(damage);
                    _ = user?.AddBattleLog($"Противнику {Entity.Name} нанесено {damage} кровотечением.");
                }
            }, CTS.Token);

            user?.AddBattleLog($"У {Entity.Name} закончилось кровотечение");
            Entity.RemoveState<BleedingState>();
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