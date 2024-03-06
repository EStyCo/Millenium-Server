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
        private readonly PlaceService placeService;
        private readonly MonsterService monsterService;
        private readonly Router router;
        public UserStore UserStore { get; set; }
        public HP HP { get; set; }
        public bool IsShowMenu { get; set; } = false;

        private int targetId = 999;
        public List<Monster> Monsters { get; set; }
        public List<string> Skills { get; set; } = new List<string>() { "Skill 1", "Skill 2", "Skill 3", "Skill 4", "Skill 5", "Skill 6" };

        public ICommand GoToTownCommand { get; set; }
        public ICommand AddMonsterCommand { get; set; }
        public ICommand DeleteMonsterCommand { get; set; }
        public ICommand SelectMonsterCommand { get; set; }
        public ICommand ShowMovementCommand { get; set; }
        public ICommand ShowCharacterCommand { get; set; }
        public ICommand ShowSpellBookCommand { get; set; }
        public ICommand ShowInventoryCommand { get; set; }

        public GladeViewModel(UserStore _userStore,
                              MonsterService _monsterService,
                              Router _router, 
                              HP _HP)
        {
            placeService = new(this);
            monsterService = _monsterService;
            router = _router;
            UserStore = _userStore;
            HP = _HP;
            Monsters = new();

            GoToTownCommand = new Command(async () => await GoToTown());
            AddMonsterCommand = new Command(async () => await AddMonster());
            DeleteMonsterCommand = new Command<int>(async (id) => await DeleteMonster(id));
            SelectMonsterCommand = new Command<int>(async (id) => await SelectMonster(id));
            ShowMovementCommand = new Command(async () => await ShowMovement());
            ShowCharacterCommand = new Command(async () => await ShowCharacter());
            ShowSpellBookCommand = new Command(async () => await ShowSpellBook());
            ShowInventoryCommand = new Command(async () => await ShowInventory());


            LoadMonsters();
        }

        private async Task SelectMonster(int id)
        {
            foreach (var item in Monsters)
            {
                item.IsTarget = false;
            }

            var monster = Monsters.FirstOrDefault(x => x.Id == id);

            if(monster != null)
            { 
                monster.IsTarget = true;
                targetId = id;
            }
        }

        public void SelectMonster()
        {
            var monster = Monsters.FirstOrDefault(x => x.Id == targetId);

            if (monster != null)
            {
                monster.IsTarget = true;
            }
        }

        private async Task LoadMonsters()
        {
            await placeService.ConnectToHub();
        }

        private async Task AddMonster()
        { 
            await monsterService.AddMonster<APIResponse>();
            //await LoadMonsters();
        }

        private async Task DeleteMonster(int id)
        {
            await monsterService.DeleteMonster<APIResponse>(id);
            //await LoadMonsters();
        }

        private async Task GoToTown()
        {
            await placeService.Disconnect();
            await router.GoToNewArea(Place.Town);
        }

        private async Task ShowMovement()
        { 
            IsShowMenu = !IsShowMenu;
        }

        private async Task ShowCharacter()
        {
            await router.GoToModalArea(ModalArea.Character);
        }

        private async Task ShowSpellBook()
        {
            await router.GoToModalArea(ModalArea.SpellBook);
        }
        private async Task ShowInventory()
        {
            await router.GoToModalArea(ModalArea.Inventory);
        }
    }
}
