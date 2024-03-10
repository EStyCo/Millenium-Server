using Client.MVVM.Model;
using Client.MVVM.Model.Utilities;
using Client.MVVM.View;
using Client.Services;
using Microsoft.Maui.Controls;
using PropertyChanged;
using System.Windows.Input;

namespace Client.MVVM.ViewModel.Town
{
    [AddINotifyPropertyChangedInterface]
    public class TownViewModel
    {
        public Router Router { get; set; }
        public UserStore UserStore {  get; set; }


        private readonly TravelService travelService;
        public ICommand BreakCharacterCommand { get; set; }


        public TownViewModel(UserStore _userStore, 
                             TravelService _travelService,
                             Router _Router) 
        {
            UserStore = _userStore;
            travelService = _travelService;
            Router = _Router;

            BreakCharacterCommand = new Command(async () => await BreakCharacter());
        }

        private async Task BreakCharacter()
        {
            await travelService.BreakChar<APIResponse>(UserStore.Character.CharacterName);
        }
    }
}
