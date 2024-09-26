using Microsoft.AspNetCore.SignalR;
using Server.Hubs;
using Server.Hubs.Locations.BasePlaces;
using Server.Models.Handlers.Stats;
using Server.Models.Modifiers.Additional;
using Server.Models.Modifiers.Increased;
using Server.Models.Utilities;

namespace Server.Models.Handlers.Vitality
{
    public class UserVitalityHandler : VitalityHandler
    {
        private readonly IHubContext<UserStorage> hubContext;
        public string ConnectionId { get; set; }
        private readonly UserStatsHandler stats;
        private readonly ModifiersHandler modifiers;
        public event Action? OnVitalityChanged;

        public UserVitalityHandler(IHubContext<UserStorage> _hubContext,
                                   string connectionId,
                                   UserStatsHandler _stats,
                                   ModifiersHandler _modifiers)
        {
            hubContext = _hubContext;
            ConnectionId = connectionId;
            stats = _stats;
            modifiers = _modifiers;
            _ = StartVitalityConnection();
        }


        public async Task StartVitalityConnection()
        {
            MaxHP = GetMaxHP();
            CurrentHP = MaxHP;
            while (true)
            {
                await Task.Delay(500);
                _ = SendHP();
                OnVitalityChanged?.Invoke();
            }
        }

        private async Task SendHP()
        {
            int[] sendHP = { MaxHP, MaxHP };
            if (CurrentHP < MaxHP)
                sendHP[0] = CurrentHP += RegenRateHP;
            if (ConnectionId != string.Empty)
                await hubContext.Clients.Client(ConnectionId).SendAsync("UpdateHP", sendHP);
        }

        public override void TakeDamage(int damage)
        {
            if (CurrentHP - damage <= 0)
                CurrentHP = 0;
            else
                CurrentHP -= damage;
        }

        public override void TakeHealing(int heal)
        {
            int temp = CurrentHP + heal;
            if (temp >= MaxHP)
                CurrentHP = MaxHP;
            else
                CurrentHP = temp;
        }

        public override void ResetValues()
        {
            var tempHP = CurrentHP;
            MaxHP = GetMaxHP();
            RegenRateHP = (int)(Math.Round(MaxHP / 60.0) * 0.5);

            if (CurrentHP > MaxHP)
                CurrentHP = MaxHP;
            else
                CurrentHP = tempHP;
        }

        private int GetMaxHP()
        {
            var additionalHP = modifiers.Get<AdditionalHPModifier>();
            var increaseHP = modifiers.Get<IncreasedHPModifier>();
            var result = (int)(100 + (stats.Vitality * 0.20) + (stats.Strength + stats.Agility + stats.Level));
            if (additionalHP != null && increaseHP != null)
            {
                result += additionalHP.Value;
                result += (int)Math.Round((double)(result * increaseHP.Value) / 100);
            }
            return result;
        }
    }
}
