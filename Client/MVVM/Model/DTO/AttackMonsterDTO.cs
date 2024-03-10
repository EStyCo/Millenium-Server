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
        public string NameCharacter { get; set; } = string.Empty;
        public string SkillNaming { get; set; } = string.Empty;
    }
}
