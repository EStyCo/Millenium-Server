using Microsoft.AspNetCore.SignalR;
using Server.Models.EntityFramework;
using Server.Models.Handlers;
using Server.Models.Interfaces;
using Server.Models.Monsters.States;
using Server.Models.Skills;
using Server.Models.Spells;
using Server.Models.Spells.Models;
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
        public override StatsHandler Stats { get; protected set; }
        public override VitalityHandler Vitality { get; protected set; }
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

                skill.IsReady = false;
                skill.RestSeconds = skill.CoolDown;

                if (skill is not Rest)
                {
                    StartGlobalRest();
                }

                _ = StartRestSkill(skill);

                /*if (resultUse != null)
                    await AddBattleLog(resultUse);*/
            }
        }

        private async Task StartRestSkill(Spell skill)
        {
            while (skill.RestSeconds > 0)
            {
                await Task.Delay(1000);
                skill.RestSeconds -= 1;
            }
            skill.RestSeconds = 0;
            skill.IsReady = true;
        }

        private void StartGlobalRest()
        {
            foreach (var skill in ActiveSkills)
            {
                if (skill.RestSeconds <= 5 && !skill.IsReady)
                {
                    skill.RestSeconds = 5;
                    skill.IsReady = false;
                    _ = StartRestSkill(skill);
                }
                else if (skill.IsReady)
                {
                    skill.RestSeconds = 5;
                    skill.IsReady = false;
                    _ = StartRestSkill(skill);
                }
            }
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

        public async void UpdateStates()
        {
            if (ConnectionId != string.Empty)
            {
                await hubContext.Clients.Client(ConnectionId).SendAsync("UpdateBuffBar", States.Keys.Select(x => x.ToJson()).ToList());
            }
        }
    }
}
