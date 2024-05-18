using Microsoft.AspNetCore.SignalR;
using Server.Hubs;
using Server.Models.DTO;
using Server.Models.EntityFramework;
using Server.Models.Handlers;
using Server.Models.Skills;
using Server.Models.Utilities;

namespace Server.Models
{
    public class ActiveUser : Entity
    {
        public string ConnectionId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public StatsHandler Stats { get; private set; }
        public VitalityHandler Vitality { get; private set; }
        public bool IsReadyCast { get; set; } = true;
        public int GlobalRestSeconds { get; set; }
        public List<Spell> ActiveSkills { get; set; } = new();

        public ActiveUser(IHubContext<UserStorage> _hubContext,
                          Stats stats,
                          string _name)
        {
            Name = _name;
            Stats = new(stats);
            Vitality = new(_hubContext, Stats, ConnectionId);
        }

        public void CreateSpellList(Character character)
        {
            ActiveSkills = new SkillCollection().CreateSkillList(new() {character.Spell1, character.Spell2, character.Spell3, character.Spell4, character.Spell5 });
        }

        public async void UseSkill(SpellType type, params Entity[] target)
        {
            var skill = ActiveSkills.FirstOrDefault(x => x.GetType() == new SkillCollection().GetSkillType(type));

            if (skill != null)
            {
                await skill.Use(this, target);

                skill.IsReady = false;
                skill.RestSeconds = skill.CoolDown;

                _ = StartRestSkill(skill);
                StartGlobalRest();
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
    }
}
