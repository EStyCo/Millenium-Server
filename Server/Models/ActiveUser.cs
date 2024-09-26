using Server.Models.Handlers.Vitality;
using Server.EntityFramework.Models;
using Microsoft.AspNetCore.SignalR;
using Server.Models.Handlers.Stats;
using Server.Models.Spells.Models;
using Server.Models.Spells.States;
using I = Server.Models.Inventory;
using Server.Models.Utilities;
using Server.Models.Handlers;
using Server.Models.Spells;
using Server.Hubs.DTO;
using System.Text;
using Server.Hubs;

namespace Server.Models
{
    public class ActiveUser : Entity
    {
        private IHubContext<UserStorage> hubContext;
        public string ConnectionId { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public override bool CanAttack { get; set; } = true;
        public int GlobalRestSeconds { get; set; } = 0;
        public override StatsHandler Stats { get; set; }
        public I.Inventory Inventory { get; set; }
        public override ModifiersHandler Modifiers { get; set; }
        public override VitalityHandler Vitality { get; protected set; }
        public List<Spell> ActiveSkills { get; set; } = new();
        public override Dictionary<State, CancellationTokenSource> States { get; protected set; } = new();
        public List<BattleLog> BattleLogs { get; set; } = new();

        public override void UseSpell(SpellType type, params Entity[] target)
        {
            var skill = ActiveSkills.FirstOrDefault(x => x.SpellType == type);

            if (skill != null && CanAttack)
            {
                skill.Use(this, target);
                if (skill is not Rest) StartGlobalRest();
            }
        }

        public void ChangeHubContext(IHubContext<UserStorage> _hubContext)
        {
            hubContext = _hubContext;
        }

        public void Initialize(CharacterEF character)
        {
            Name = character.Name;
            ImagePath = "characters/titan.png";
            Place = character.Place;
            Stats = new UserStatsHandler();
            Stats.SetStats(character.Stats);

            Modifiers = new ModifiersHandler();

            Vitality = new UserVitalityHandler(hubContext, ConnectionId, (UserStatsHandler)Stats, Modifiers);
            Inventory = new(ItemFactory.GetList(character.Items), this);
            ActiveSkills.AddRange(SpellFactory.Get(character.Spells));
        }

        private async void StartGlobalRest()
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
            if (BattleLogs.Count > 10) BattleLogs.RemoveAt(0);
            BattleLogs.Add(new()
            {
                Data = str,
                Time = $"{DateTime.Now.ToString("HH:mm:ss")}"
            });
            if (ConnectionId != string.Empty)
                await hubContext.Clients.Client(ConnectionId).SendAsync("UpdateLogs", BattleLogs.Last());
        }

        public override ResultUseSpell TakeDamage(int damage)
        {
            Vitality.TakeDamage(damage);

            if (Vitality.CurrentHP <= 0)
            {
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
                await hubContext.Clients
                    .Client(ConnectionId)
                    .SendAsync("UpdateBuffBar", States.Keys.Select(x => x.ToJson()).ToList());
        }

        public void ReAssembly()
        {
            (Stats as UserStatsHandler)?.ReturnToOriginalStats();
            Modifiers.ResetValues();
            Inventory.EquipItems();
            Vitality.ResetValues();
        }

        public UserOnPlace ToJson()
        {
            var stats = Stats as UserStatsHandler;
            var vitality = Vitality as UserVitalityHandler;

            return new(Name, stats.Level, vitality.CurrentHP, vitality.MaxHP, States.Keys.Select(x => x.ToJson()).ToList());
        }
    }
}