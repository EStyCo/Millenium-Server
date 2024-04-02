using Client.MVVM.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MVVM.Model.DTO
{
    public class AttackMonsterDTO
    {
        public int IdMonster { get; set; }
        public int SkillId { get; set; }
        public string NameCharacter { get; set; } = string.Empty;
        public Place Place { get; set; }
    }
}
