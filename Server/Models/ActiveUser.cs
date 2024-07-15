using Server.Hubs.Locations.BasePlaces;
using Server.Models.Handlers.Vitality;
using Microsoft.AspNetCore.SignalR;
using Server.Models.Handlers.Stats;
using Server.Models.Spells.Models;
using Server.Models.Spells.States;
using Server.Models.Utilities;
using Server.Models.Spells;
using Server.Hubs.DTO;
using System.Text;
using I = Server.Models.Inventory;
using Server.Models.Inventory;
using Server.EntityFramework.Models;
using Server.Services;

namespace Server.Models
{
    public class ActiveUser : Entity
    {
        private readonly IHubContext<UserStorage> hubContext;
        private readonly PlaceService placeService;
        public string ConnectionId { get; set; } = string.Empty;
        public override string Name { get; protected set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public override bool CanAttack { get; set; } = true;
        public int GlobalRestSeconds { get; private set; } = 0;
        public override StatsHandler Stats { get; protected set; }
        public override VitalityHandler Vitality { get; protected set; }
        public BasePlace? CurrentPlace { get; set; }
        public List<Spell> ActiveSkills { get; set; } = new();
        public List<string> BattleLogs { get; set; } = new();
        public override Dictionary<State, CancellationTokenSource> States { get; protected set; } = new();
        public I.Inventory Inventory { get; set; }

        public ActiveUser(IHubContext<UserStorage> _hubContext,
                          PlaceService _placeService,
                          Stats stats,
                          Character character,
                          List<Item> items)

        {
            hubContext = _hubContext;
            placeService = _placeService;
            Name = character.Name;
            Place = character.Place;
            Stats = new UserStatsHandler(stats);
            Vitality = new UserVitalityHandler(_hubContext, (UserStatsHandler)Stats, ConnectionId);
            CurrentPlace = placeService.GetPlace(Place) as BasePlace;
            Inventory = new(items);
        }

        public override void UseSpell(SpellType type, params Entity[] target)
        {
            var skill = ActiveSkills.FirstOrDefault(x => x.SpellType == type);

            if (skill != null && CanAttack)
            {
                skill.Use(this, target);

                if (skill is not Rest)
                {
                    _ = StartGlobalRest();
                }
            }
        }

        private async Task StartGlobalRest()
        {
            GlobalRestSeconds = 5;
            CanAttack = false;

            while (GlobalRestSeconds > 0)
            {
                await Task.Delay(1000);
                GlobalRestSeconds--;
            }

            GlobalRestSeconds = 0;
            var a = States.Keys.FirstOrDefault(x => x.GetType() == typeof(WeaknessState));
            if (a == null) CanAttack = true;
        }

        public void ResetAllSpells()
        {
            foreach (var skill in ActiveSkills)
            {
                skill.RestSeconds = 0;
                skill.IsReady = true;
            }
        }

        public void ChangeConnectionId(string connectionId)
        {
            ConnectionId = connectionId;

            var vitality = Vitality as UserVitalityHandler;
            if (vitality != null)
            {
                vitality.ConnectionId = connectionId;
            }
        }

        public async Task AddBattleLog(string str)
        {
            StringBuilder sb = new(str);

            if (BattleLogs.Count > 10) BattleLogs.RemoveAt(0);

            BattleLogs.Add(sb.Insert(0, $"[{DateTime.Now.ToString("HH:mm:ss")}] - ").ToString());

            if (ConnectionId != string.Empty)
            {
                await hubContext.Clients.Client(ConnectionId).SendAsync("UpdateLogs", BattleLogs.Last());
            }
        }

        public override ResultUseSpell TakeDamage(int damage)
        {
            Vitality.TakeDamage(damage);

            if (Vitality.CurrentHP <= 0)
            {
                placeService.GetBattlePlace(Place)?.WeakeningPlayer(Name);
                AddState<WeaknessState>(this);
                UpdateStates();
            }

            return new(damage, Vitality.CurrentHP, Vitality.MaxHP);
        }

        public override ResultUseSpell TakeHealing(int healing)
        {
            Vitality.TakeHealing(healing);
            return new(healing, Vitality.CurrentHP, Vitality.MaxHP);
        }

        public override async void UpdateStates()
        {
            if (ConnectionId != string.Empty)
                await hubContext.Clients.Client(ConnectionId)
                                        .SendAsync("UpdateBuffBar", States.Keys.Select(x => x.ToJson())
                                        .ToList());
        }

        public UserOnPlace ToJson()
        {
            var stats = Stats as UserStatsHandler;
            var vitality = Vitality as UserVitalityHandler;

            return new(Name, stats.Level, vitality.CurrentHP, vitality.MaxHP, States.Keys.Select(x => x.ToJson()).ToList());
        }
    }
}