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
        private readonly TravelService travelService;
        private readonly VitalityService vitalityService;
        public UserStore UserStore {  get; set; }
        public HP HP { get; set; }

        public ICommand BreakCharacterCommand { get; set; }


        public TownViewModel(UserStore _userStore, 
                             VitalityService _vitalityService, 
                             TravelService _travelService,
                             Router _Router, 
                             HP _HP) 
        {
            UserStore = _userStore;
            vitalityService = _vitalityService;
            travelService = _travelService;
            Router = _Router;
            HP = _HP;

            BreakCharacterCommand = new Command(async () => await BreakCharacter());
        }

        private async Task BreakCharacter()
        {
            await travelService.BreakChar<APIResponse>(UserStore.Character.CharacterName);
        }
    }
}
