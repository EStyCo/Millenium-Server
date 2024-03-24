using AutoMapper;
using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.Services;
using PropertyChanged;
using System.Windows.Input;

namespace Client.MVVM.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class DarkWoodViewModel
    {
        public UserStore UserStore { get; set; }
        public PlaceService PlaceService { get; set; }
        public Router Router { get; set; }

        private readonly MonsterService monsterService;
        private readonly IMapper mapper;

        public ICommand AddMonsterCommand { get; set; }

        public DarkWoodViewModel(UserStore _userStore, Router _router, MonsterService _monsterService, IMapper _mapper)
        {
            UserStore = _userStore;
            Router = _router;
            monsterService = _monsterService;
            mapper = _mapper;

            AddMonsterCommand = new Command(async () => await AddMonster());

            PlaceService = new(UserStore, monsterService, "DarkWood", mapper);

            PlaceService.ConnectToHub();
        }

        private async Task AddMonster()
        {
            await monsterService.AddMonster<APIResponse>(new PlaceDTO()
            {
                Place = UserStore.Character.CurrentArea
            });
        }
    }
}
