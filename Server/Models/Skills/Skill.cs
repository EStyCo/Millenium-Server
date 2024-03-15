using Server.Models.DTO;
using static Server.Models.ActiveUser;

namespace Server.Models.Skills
{
    public abstract class Skill
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public int CoolDown { get; set; }
        public int RestSeconds { get; set; } = 0;
        public bool IsReady { get; set; } = true;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;

        protected SendRestTimeDelegate SendRestDelegate;

        public abstract Task<int> Attack(CharacterDTO c);
        public void ActivateSkill(int id, SendRestTimeDelegate sendRestDelegate)
        { 
            Id = id;
            SendRestDelegate = sendRestDelegate;
        }
    }
}
