using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.Model.Utilities;
using Newtonsoft.Json;
using PropertyChanged;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Client.Services
{
    [AddINotifyPropertyChangedInterface]
    public class Router
    {
        private readonly UserStore userStore;
        private readonly TravelService travelService;

        public PlaceService PlaceService { get; set; }

        public ICommand GoAreaCommand { get; set; }
        public ICommand GoCurrentAreaCommand { get; set; }
        public ICommand GoModalAreaCommand { get; set; }
        public ICommand ShowMoveMenuCommand { get; set; }

        public bool canBack = false;
        public bool IsShowMenu { get; set; } = false;

        public Router(UserStore _userStore, TravelService _travelService)
        {
            userStore = _userStore;
            travelService = _travelService;

            GoAreaCommand = new Command<Place>(async (area) => await GoNewArea(area));
            GoCurrentAreaCommand = new Command(async () => await GoCurrentArea());
            GoModalAreaCommand = new Command<ModalArea>(async (modalArea) => await GoModalArea(modalArea));
            ShowMoveMenuCommand = new Command(async () => await ShowMoveMenu());
        }


        public async Task GoCurrentArea()
        {
            if (PlaceService != null)
            { 
                await PlaceService.Disconnect();
            }

            var response = await travelService.GetCurrentPage<APIResponse>(new TravelDTO
            {
                CharacterName = userStore.Character.CharacterName,
                Place = userStore.Character.CurrentArea
            });

            if (response != null && response.IsSuccess)
            {
                var result = JsonConvert.DeserializeObject<TravelDTO>(Convert.ToString(response.Result));
                await Shell.Current.GoToAsync(result?.Place.ToString());
            }
        }

        public async Task GoNewArea(Place place)
        {
            var response = await travelService.PushNewPage<APIResponse>(new TravelDTO
            {
                CharacterName = userStore.Character.CharacterName,
                Place = place
            });

            if (response != null && response.IsSuccess && (bool)response.Result)
            {
                userStore.Character.CurrentArea = place;
                IsShowMenu = false;
                await Shell.Current.GoToAsync(place.ToString());
            }
            else
            {
                var error = response?.ErrorMessages;
                await Application.Current.MainPage.DisplayAlert("", $"Движение невозможно! \n{error?.FirstOrDefault()}", "Понято");
            }
        }

        public async Task GoModalArea(ModalArea area)
        {
            await Shell.Current.GoToAsync(area.ToString());
        }

        private async Task ShowMoveMenu()
        {
            IsShowMenu = !IsShowMenu;
        }
    }
}
