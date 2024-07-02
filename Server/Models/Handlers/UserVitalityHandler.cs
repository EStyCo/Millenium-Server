using Microsoft.AspNetCore.SignalR;
using Server.Hubs.Locations.BasePlaces;
using Server.Models.Utilities;

namespace Server.Models.Handlers
{
    public class UserVitalityHandler : VitalityHandler
    {
        private readonly IHubContext<UserStorage> hubContext;
        private readonly UserStatsHandler stats;
        /*public delegate void VitalityDelegate();
        public VitalityDelegate OnVitalityChanged;*/

        public event Action OnVitalityChanged;
        public string ConnectionId { get; set; }

        private int maxHP;
        private int maxMP;
        private int regenRateHP;
        private int regenRateMP;
        public override int CurrentHP { get; protected set; } = 0;
        public int CurrentMP { get; private set; } = 0;
        public override int MaxHP { get { return maxHP = Consider.MaxHP(stats); } set { maxHP = value; } }
        public int MaxMP { get { return maxMP = Consider.MaxMP(stats); } }
        public int RegenRateHP { get { return regenRateHP = Consider.RegenRateHP(this, stats); } }
        public int RegenRateMP { get { return regenRateMP = Consider.RegenRateMP(stats); } }

        public UserVitalityHandler(IHubContext<UserStorage> _hubContext, 
                                   UserStatsHandler _stats,
                                   string connectionId)
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

                await Task.Delay(500);

                OnVitalityChanged?.Invoke();
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

        public override void TakeDamage(int damage)
        {
            if (CurrentHP - damage <= 0)
            {
                CurrentHP = 0;
            }
            else
            {
                CurrentHP -= damage;
            }
        }

        public override void TakeHealing(int heal)
        {
            int temp = CurrentHP + heal;

            if (temp >= MaxHP)
            {
                CurrentHP = maxHP;
            }
            else
            {
                CurrentHP = temp;
            }
        }
    }
}
