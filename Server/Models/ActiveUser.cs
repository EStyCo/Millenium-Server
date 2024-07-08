using Microsoft.AspNetCore.SignalR;
using Server.Hubs.DTO;
using Server.Hubs.Locations.BasePlaces;
using Server.Models.EntityFramework;
using Server.Models.Handlers.Stats;
using Server.Models.Handlers.Vitality;
using Server.Models.Interfaces;
using Server.Models.Skills;
using Server.Models.Spells;
using Server.Models.Spells.Models;
using Server.Models.Spells.States;
using Server.Models.Utilities;
using System.Text;

namespace Server.Models
{
    public class ActiveUser : Entity
    {
        private readonly IHubContext<UserStorage> hubContext;
        private readonly IAreaStorage areaStorage;
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

        public ActiveUser(IHubContext<UserStorage> _hubContext,
                          IAreaStorage _areaStorage,
                          Stats stats,
                          Character character)
        {
            hubContext = _hubContext;
            areaStorage = _areaStorage;
            Name = character.Name;
            Place = character.Place;
            Stats = new UserStatsHandler(stats);
            CurrentPlace = areaStorage.GetPlace(Place) as BasePlace;
            Vitality = new UserVitalityHandler(_hubContext, (UserStatsHandler)Stats, ConnectionId);
        }

        public void CreateSpellList(Character character)
        {
            ActiveSkills = new SkillCollection().CreateSkillList(new() { character.Spell1, character.Spell2, character.Spell3, character.Spell4, character.Spell5 });
        }

        public override void UseSpell(SpellType type, params Entity[] target)
        {
            var skill = ActiveSkills.FirstOrDefault(x => x.GetType() == new SkillCollection().PickSkill(type).GetType());

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
                areaStorage.GetBattlePlace(Place)?.WeakeningPlayer(Name);
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
            {
                await hubContext.Clients.Client(ConnectionId).SendAsync("UpdateBuffBar", States.Keys.Select(x => x.ToJson()).ToList());
            }
        }

        public UserOnPlace ToJson()
        {
            var stats = Stats as UserStatsHandler;
            var vitality = Vitality as UserVitalityHandler;

            return new(Name, stats.Level, vitality.CurrentHP, vitality.MaxHP, States.Keys.Select(x => x.ToJson()).ToList());
        }
    }
}