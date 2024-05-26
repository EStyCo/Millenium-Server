using Microsoft.AspNetCore.SignalR;
using Server.Models.EntityFramework;
using Server.Models.Handlers;
using Server.Models.Skills;
using Server.Models.Utilities;
using System.Text;

namespace Server.Models
{
    public class ActiveUser : Entity
    {
        private readonly IHubContext<UserStorage> hubContext;
        public string ConnectionId { get; set; } = string.Empty;
        public override string Name { get; protected set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public override BaseStatsHandler Stats { get; protected set; }
        public VitalityHandler Vitality { get; private set; }
        public bool IsReadyCast { get; set; } = true;
        public int GlobalRestSeconds { get; set; }
        public List<Spell> ActiveSkills { get; set; } = new();
        public List<string> BattleLogs { get; set; } = new();

        public ActiveUser(IHubContext<UserStorage> _hubContext,
                          Stats stats,
                          Character character)
        {
            hubContext = _hubContext;
            Name = character.Name;
            Place = character.Place;
            Stats = new UserStatsHandler(stats);
            Vitality = new(_hubContext, (UserStatsHandler)Stats, ConnectionId);
        }

        public void CreateSpellList(Character character)
        {
            ActiveSkills = new SkillCollection().CreateSkillList(new() { character.Spell1, character.Spell2, character.Spell3, character.Spell4, character.Spell5 });
        }

        public override async void UseSkill(SpellType type, params Entity[] target)
        {
            var skill = ActiveSkills.FirstOrDefault(x => x.GetType() == new SkillCollection().GetSkillType(type));

            if (skill != null)
            {
                var resultUse = skill.Use(this, target);

                skill.IsReady = false;
                skill.RestSeconds = skill.CoolDown;

                _ = StartRestSkill(skill);
                StartGlobalRest();

                if (resultUse != null)
                    await AddBattleLog($"Вы ударили {resultUse.Item2} на {resultUse.Item1} урона.");
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

        public void ChangeConnectionId(string connectionId)
        {
            ConnectionId = connectionId;
            Vitality.ConnectionId = connectionId;
        }

        public async Task AddBattleLog(string str)
        {
            StringBuilder sb = new(str);

            if (BattleLogs.Count > 10) BattleLogs.RemoveAt(0);

            BattleLogs.Add(sb.Insert(0,$"[{DateTime.Now.ToString("HH:mm:ss")}] - ").ToString());

            if (ConnectionId != string.Empty)
            {
                await hubContext.Clients.Client(ConnectionId).SendAsync("UpdateLogs", BattleLogs);
            }
        }

        public override int TakeDamage(int damage)
        {
            Vitality.TakeDamage(damage);

            return damage;
        }
    }
}
