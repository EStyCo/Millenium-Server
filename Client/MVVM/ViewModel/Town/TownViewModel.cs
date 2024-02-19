using Client.MVVM.Model;
using Client.MVVM.Model.Utilities;
using Client.MVVM.View;
using Client.Services;
using PropertyChanged;
using System.Windows.Input;

namespace Client.MVVM.ViewModel.Town
{
    [AddINotifyPropertyChangedInterface]
    public class TownViewModel
    {
        private readonly Router router;
        private readonly TravelService travelService;
        private readonly VitalityService vitalityService;
        public UserStore UserStore {  get; set; }
        public HP HP { get; set; }

        public ICommand BreakCharacterCommand { get; set; }

        public ICommand GoToGladeCommand { get; set; }
        public ICommand ShowCharacterCommand { get; set; }
        public ICommand ShowSpellBookCommand { get; set; }
        public ICommand ShowInventoryCommand { get; set; }

        public TownViewModel(UserStore _userStore, Router _router, HP _HP, VitalityService _vitalityService, TravelService _travelService)
        {
            UserStore = _userStore;
            vitalityService = _vitalityService;
            travelService = _travelService;
            HP = _HP;

            BreakCharacterCommand = new Command(async () => await BreakCharacter());

            GoToGladeCommand = new Command(async () => await GoToGlade());
            ShowCharacterCommand = new Command(async () => await ShowCharacter());
            ShowSpellBookCommand = new Command(async () => await ShowSpellBook());
            ShowInventoryCommand = new Command(async () => await ShowInventory());
            router = _router;

            //ConnectionHub();
        }

        /*private async Task ConnectionHub()
        {
            await vitalityService.ConnectionToHub();
        }*/

        private async Task BreakCharacter()
        {
            await travelService.BreakChar<APIResponse>(UserStore.Character.CharacterName);
        }

        private async Task GoToGlade()
        {
            await router.GoToNewArea(Area.Glade);
            //await Shell.Current.GoToAsync(Area.Glade.ToString());
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
