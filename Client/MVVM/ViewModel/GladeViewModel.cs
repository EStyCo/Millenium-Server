using Client.MVVM.Model;
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
        private readonly UserStore userStore;
        private readonly MonsterService monsterService;
        private readonly Router router;

        public List<Monster> Monsters { get; set; }

        public ICommand GoToTownCommand { get; set; }
        public ICommand AddMonsterCommand { get; set; }
        public ICommand DeleteMonsterCommand { get; set; }

        public GladeViewModel(UserStore _userStore, 
                              Router _router,
                              MonsterService _monsterService)
        {
            userStore = _userStore;
            monsterService = _monsterService;
            router = _router;

            Monsters = new();

            GoToTownCommand = new Command(async () => await GoToTown());
            AddMonsterCommand = new Command(async () => await AddMonster());
            DeleteMonsterCommand = new Command<int>(async (id) => await DeleteMonster(id));

            LoadMonsters();
        }

        private async Task LoadMonsters()
        {
            var response = await monsterService.GetMonsters<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                Monsters = JsonConvert.DeserializeObject<List<Monster>>(Convert.ToString(response.Result));
            }
        }

        private async Task AddMonster()
        { 
            await monsterService.AddMonster<APIResponse>();
            await LoadMonsters();
        }

        private async Task DeleteMonster(int id)
        {
            await monsterService.DeleteMonster<APIResponse>(id);
            await LoadMonsters();
        }

        private async Task GoToTown()
        {
            await router.GoToNewArea(Area.Town);
            //await Shell.Current.GoToAsync(Area.Town.ToString());
        }
    }
}
