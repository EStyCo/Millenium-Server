using Microsoft.AspNetCore.SignalR;
using Server.Hubs;
using Server.Models.DTO;
using Server.Models.Skills;
using Server.Models.Utilities;

namespace Server.Models
{
    public class ActiveUser
    {
        public string ConnectionId { get; set; } = string.Empty;
        public int CurrentHP { get; set; }
        public int maxHP;
        public int MaxHP { get { return maxHP = Consider.MaxHP(Character); } }
        public int regenRateHP;
        public int RegenRateHP { get { return regenRateHP = Consider.RegenRateHP(Character); } }
        public int CurrentMP { get; set; }
        public int maxMP;
        public int MaxMP { get { return maxMP = Consider.MaxMP(Character); } }
        public int regenRateMP;


        public int RegenRateMP { get { return regenRateMP = Consider.RegenRateMP(Character); } }
        public CharacterDTO Character { get; set; }
        public List<Simple> ActiveSkills { get; set; } = new List<Simple> { new Simple(), new Simple(), new Simple(), new Simple() };

        private readonly IHubContext<UserStorage> hubContext;

        public ActiveUser(IHubContext<UserStorage> _hubContext)
        {
            hubContext = _hubContext;

        }

        public async Task StartHub()
        {

            while (true)
            {
                await SendHP();
                await SendMP();

                await Task.Delay(1000);
            }
        }

        public async Task SendHP()
        {
            int[] sendHP = { maxHP, maxHP };

            if (CurrentHP < MaxHP)
            {
                sendHP[0] = CurrentHP += RegenRateHP;
            }

            await hubContext.Clients.Client(ConnectionId).SendAsync("UpdateHP", sendHP);
        }

        public async Task SendMP()
        {
            int sendMP = maxMP;

            if (CurrentMP < MaxMP)
            {
                sendMP = CurrentMP += RegenRateMP;
            }

            await hubContext.Clients.Client(ConnectionId).SendAsync("UpdateMP", sendMP);
        }
    }
}
