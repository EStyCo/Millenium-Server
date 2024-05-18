using Microsoft.AspNetCore.SignalR;
using Server.Models.Interfaces;
using Server.Models.Utilities;

namespace Server.Models.Handlers
{
    public class VitalityHandler : IVitalityHandler
    {
        private readonly IHubContext<UserStorage> hubContext;
        private readonly StatsHandler stats;
        public string ConnectionId { get; set; }

        private int maxHP;
        private int maxMP;
        private int regenRateHP;
        private int regenRateMP;
        public int CurrentHP { get; private set; } = 0;
        public int CurrentMP { get; private set; } = 0;
        public int MaxHP { get { return maxHP = Consider.MaxHP(stats); } }
        public int MaxMP { get { return maxMP = Consider.MaxMP(stats); } }
        public int RegenRateHP { get { return regenRateHP = Consider.RegenRateHP(stats); } }
        public int RegenRateMP { get { return regenRateMP = Consider.RegenRateMP(stats); } }

        public VitalityHandler(IHubContext<UserStorage> _hubContext, StatsHandler _stats, string connectionId)
        {
            hubContext = _hubContext;
            stats = _stats;
            ConnectionId = connectionId;

            _ = StartVitalityConnection();
        }

        public async Task StartVitalityConnection()
        {
            while (true)
            {
                _ = SendHP();
                _ = SendMP();

                await Task.Delay(1000);
            }
        }

        private async Task SendHP()
        {
            int[] sendHP = { maxHP, maxHP };

            if (CurrentHP < MaxHP)
            {
                sendHP[0] = CurrentHP += RegenRateHP;
            }

            if (ConnectionId != string.Empty)
            {
                await hubContext.Clients.Client(ConnectionId).SendAsync("UpdateHP", sendHP);
            }
        }

        private async Task SendMP()
        {
            int[] sendMP = { maxMP, maxMP };

            if (CurrentMP < MaxMP)
            {
                sendMP[0] = CurrentMP += RegenRateMP;
            }

            if (ConnectionId != string.Empty)
            {
                await hubContext.Clients.Client(ConnectionId).SendAsync("UpdateMP", sendMP);
            }
        }
    }
}
