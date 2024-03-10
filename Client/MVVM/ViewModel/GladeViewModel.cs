using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.Model.Utilities;
using Client.MVVM.View.Town;
using Client.Services;
using Newtonsoft.Json;
using PropertyChanged;
using System.Linq.Expressions;
using System.Windows.Input;

namespace Client.MVVM.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class GladeViewModel 
    {
        public PlaceService PlaceService { get; set; }
        private readonly UserStore userStore;
        private readonly MonsterService monsterService;
        public Router Router { get; set; }
        public HP HP { get; set; }
        public List<string> Skills { get; set; } = new List<string>() { "Skill 1", "Skill 2", "Skill 3", "Skill 4", "Skill 5", "Skill 6" };

        public ICommand GoToTownCommand { get; set; }
        public ICommand AddMonsterCommand { get; set; }
        public ICommand DeleteMonsterCommand { get; set; }
        //public ICommand SelectMonsterCommand { get; set; }

        public GladeViewModel(MonsterService _monsterService,
                              UserStore _userStore,
                              Router _router,
                              HP _HP)
        {
            userStore = _userStore;
            monsterService = _monsterService;
            Router = _router;
            HP = _HP;
            PlaceService = new(this, userStore, monsterService);

            AddMonsterCommand = new Command(async () => await AddMonster());
            DeleteMonsterCommand = new Command<int>(async (id) => await DeleteMonster(id));
            //SelectMonsterCommand = new Command<int>(async (id) => await SelectMonster(id));

            LoadMonsters();
        }

        /*private async Task SelectMonster(int id)
        {
            foreach (var item in placeService.Monsters)
            {
                item.IsTarget = false;
            }

            var monster = placeService.Monsters.FirstOrDefault(x => x.Id == id);

            if(monster != null)
            { 
                monster.IsTarget = true;
                placeService.targetId = id;
            }
        }*/

        /*public void SelectMonster()
        {
            var monster = Monsters.FirstOrDefault(x => x.Id == targetId);

            if (monster != null)
            {
                monster.IsTarget = true;
            }
        }*/

        private async Task LoadMonsters()
        {
            await PlaceService.ConnectToHub();
        }

        private async Task AddMonster()
        { 
            await monsterService.AddMonster<APIResponse>();
        }

        private async Task DeleteMonster(int id)
        {
            await monsterService.DeleteMonster<APIResponse>(id);
        }
    }
}
