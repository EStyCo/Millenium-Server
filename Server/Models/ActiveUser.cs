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
        public int CurrentHP { get; set; } = 0;
        public int maxHP;
        public int MaxHP { get { return maxHP = Consider.MaxHP(Character); } }
        public int regenRateHP;
        public int RegenRateHP { get { return regenRateHP = Consider.RegenRateHP(Character); } }
        public int CurrentMP { get; set; }
        public int maxMP;
        public int MaxMP { get { return maxMP = Consider.MaxMP(Character); } }
        public int regenRateMP;


        public delegate Task SendRestTimeDelegate(int id, int time);
        public int RegenRateMP { get { return regenRateMP = Consider.RegenRateMP(Character); } }
        public CharacterDTO Character { get; set; }
        public List<Skill> ActiveSkills { get; set; }

        private readonly IHubContext<UserStorage> hubContext;

        public ActiveUser(IHubContext<UserStorage> _hubContext, 
                          CharacterDTO _character)
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

        public async Task UpdateSpellList(CharacterDTO character)
        {
            await CreateSpellList(character);

            List<SpellDTO> spellList = new();

            foreach (Skill spell in ActiveSkills)
            {
                SpellDTO spellDTO = new();
                spellDTO.Id = spell.Id;
                spellDTO.Name = spell.Name;
                spellDTO.IsReady = spell.IsReady;
                spellDTO.CoolDown = spell.RestSeconds;

                spellList.Add(spellDTO);
            }
            await hubContext.Clients.Client(ConnectionId).SendAsync("UpdateSpellList", spellList);
        }

        public async Task SendRestTime(int id, int time)
        {
            var spell = ActiveSkills.FirstOrDefault(x => x.Id == id);
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
            int[] sendMP = { maxMP, maxMP };

            if (CurrentMP < MaxMP)
            {
                sendMP[0] = CurrentMP += RegenRateMP;
            }

            await hubContext.Clients.Client(ConnectionId).SendAsync("UpdateMP", sendMP);
        }

        private async Task CreateSpellList(CharacterDTO character)
        {
            await Task.Delay(10);

            List<SkillType> skills = new() {character.Skill1, 
                                            character.Skill2, 
                                            character.Skill3, 
                                            character.Skill4, 
                                            character.Skill5 };

            SendRestTimeDelegate sendDelegate;
            sendDelegate = SendRestTime;

            ActiveSkills = new SkillCollection().CreateSkillList(skills, sendDelegate);
        }
    }
}
