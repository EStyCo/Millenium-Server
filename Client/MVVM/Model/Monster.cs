using Client.MVVM.Model.DTO;
using Client.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.MVVM.Model
{
    [AddINotifyPropertyChangedInterface]
    public class Monster
    {
        public int Id { get; set; }
        public bool IsTarget { get; set; } = false;
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;


        private readonly UserStore userStore;
        private readonly MonsterService monsterService;
        //public ICommand AttackTargetCommand { get; set; }

        public Monster(UserStore _userStore, MonsterService _monsterService)
        {
            userStore = _userStore;
            monsterService = _monsterService;
            //AttackTargetCommand = new Command(async () => await Attack());
        }

        private async Task Attack()
        {
            AttackMonsterDTO attack = new();
            attack.IdMonster = Id;
            attack.NameCharacter = userStore.Character.CharacterName;
            attack.SkillNaming = "Удар с правой";

            await monsterService.AttackMonster<APIResponse>(attack);
        }
    }
}
