using Microsoft.AspNetCore.SignalR;
using Server.Hubs;
using Server.Models.DTO;
using Server.Models.Skills;
using Server.Models.Utilities;

namespace Server.Models
{
    public class ActiveUser : Entity
    {
        public string ConnectionId { get; set; } = string.Empty;
        public string UserIdentificator { get; set; } = string.Empty;
        public int CurrentHP { get; set; } = 0;
        public int maxHP;
        public int MaxHP { get { return maxHP = Consider.MaxHP(Character); } }
        public int regenRateHP;
        public int RegenRateHP { get { return regenRateHP = Consider.RegenRateHP(Character); } }
        public int CurrentMP { get; set; }
        public int maxMP;
        public int MaxMP { get { return maxMP = Consider.MaxMP(Character); } }
        public int regenRateMP;


        public bool IsReadyCast { get; set; } = true;
        public int GlobalRestSeconds { get; set; }

        public int RegenRateMP { get { return regenRateMP = Consider.RegenRateMP(Character); } }
        public Character Character { get; set; }
        public List<Spell> ActiveSkills { get; set; }

        private readonly IHubContext<UserStorage> hubContext;

        public ActiveUser(IHubContext<UserStorage> _hubContext,
                          Character _character)
        {
            hubContext = _hubContext;
            Character = _character;
        }

        public async Task StartVitalityConnection()
        {
            while (true)
            {
                await SendHP();
                await SendMP();

                await Task.Delay(1000);
            }
        }

        public async Task UpdateSpellList(Character character)
        {
            await CreateSpellList(character);

            List<SpellDTO> spellList = new();

            foreach (Spell spell in ActiveSkills)
            {
                SpellDTO spellDTO = new();
                spellDTO.SpellType = spell.SpellType;
                spellDTO.Name = spell.Name;
                spellDTO.IsReady = spell.IsReady;
                spellDTO.CoolDown = spell.RestSeconds;

                spellList.Add(spellDTO);
            }
            //await hubContext.Clients.Client(ConnectionId).SendAsync("UpdateSpellList", spellList);
        }

        public async Task SendRestTime(int id, int time)
        {
            /*var spell = ActiveSkills.FirstOrDefault(x => x.Id == id);
            if (spell != null)
            {
                SpellDTO spellDTO = new()
                {
                    Id = spell.Id,
                    Name = spell.Name,
                    IsReady = spell.IsReady,
                    CoolDown = spell.RestSeconds,
                };

                await hubContext.Clients.Client(ConnectionId).SendAsync("UpdateSpell", spellDTO);
            }*/
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
            int[] sendMP = { maxMP, maxMP };

            if (CurrentMP < MaxMP)
            {
                sendMP[0] = CurrentMP += RegenRateMP;
            }

            await hubContext.Clients.Client(ConnectionId).SendAsync("UpdateMP", sendMP);
        }

        private async Task CreateSpellList(Character character)
        {
            await Task.Delay(10);

            List<SpellType> spells = new() {character.Spell1,
                                            character.Spell2,
                                            character.Spell3,
                                            character.Spell4,
                                            character.Spell5 };

            ActiveSkills = new SkillCollection().CreateSkillList(spells);
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
                _ = StartGlobalRest();
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

        private async Task StartGlobalRest()
        {
            foreach (var skill in ActiveSkills)
            {
                if (skill.RestSeconds <= 5 && !skill.IsReady)
                {
                    skill.RestSeconds = 5;
                    skill.IsReady = false;
                    _ = StartRestSkill(skill);
                }
                else if(skill.IsReady)
                {
                    skill.RestSeconds = 5;
                    skill.IsReady = false;
                    _ = StartRestSkill(skill);
                }
            }
        }

        public List<RestTimeDTO> GetRestTime()
        {
            List<RestTimeDTO> listRestTime = new();

            foreach (var skill in ActiveSkills)
            {
                listRestTime.Add(new RestTimeDTO
                {
                    SpellType = skill.SpellType,
                    Name = skill.Name,
                    RestSeconds = skill.RestSeconds,
                    IsReady = skill.IsReady,
                });
            }

            return listRestTime;
        }
    }
}
