using AutoMapper;
using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.Services;
using PropertyChanged;
using System.Windows.Input;

namespace Client.MVVM.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class GladeViewModel 
    {
        public PlaceService PlaceService { get; set; }
        public UserStore UserStore { get; set; }
        public Router Router { get; set; }

        private readonly MonsterService monsterService;
        private readonly IMapper mapper;

        public ICommand GoToTownCommand { get; set; }
        public ICommand AddMonsterCommand { get; set; }
        public ICommand DeleteMonsterCommand { get; set; }

        public GladeViewModel(MonsterService _monsterService,
                              UserStore _UserStore,
                              Router _router,
                              IMapper mapper)
        {
            UserStore = _UserStore;
            monsterService = _monsterService;
            Router = _router;
            PlaceService = new(UserStore, monsterService, "Glade", mapper);

            AddMonsterCommand = new Command(async () => await AddMonster());
            DeleteMonsterCommand = new Command<int>(async (id) => await DeleteMonster(id));

            LoadMonsters();
        }

        private async Task LoadMonsters()
        {
            await PlaceService.ConnectToHub();
        }

        private async Task AddMonster()
        { 
            await monsterService.AddMonster<APIResponse>(new PlaceDTO() 
            { 
                Place = UserStore.Character.CurrentArea 
            });
        }

        private async Task DeleteMonster(int id)
        {
            await monsterService.DeleteMonster<APIResponse>(new DeleteMonsterDTO() 
            { 
                Place = UserStore.Character.CurrentArea,
                Id = id
            });
        }
    }
}
