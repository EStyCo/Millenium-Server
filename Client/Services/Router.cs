using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.Model.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class Router
    {
        private readonly UserStore userStore;
        private readonly TravelService travelService;

        public bool canBack = false;

        public Router(UserStore _userStore, TravelService _travelService)
        {
            userStore = _userStore;
            travelService = _travelService;
        }

        public async Task GoToCurrentArea()
        {
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

        public async Task GoToNewArea(Place place)
        {
            var response = await travelService.PushNewPage<APIResponse>(new TravelDTO
            {
                CharacterName = userStore.Character.CharacterName,
                Place = place
            });

            if (response != null && response.IsSuccess && (bool)response.Result)
            {
                userStore.Character.CurrentArea = place;
                await Shell.Current.GoToAsync(place.ToString());
            }
            else if(response.ErrorMessages.Count > 0)
            {
                var error = response.ErrorMessages;
                await Application.Current.MainPage.DisplayAlert("", $"{error.FirstOrDefault()}", "Понято");
            }
        }

        public async Task GoToModalArea(ModalArea area)
        {
            await Shell.Current.GoToAsync(area.ToString());
        }
    }
}
