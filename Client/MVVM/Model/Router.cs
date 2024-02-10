using Client.MVVM.Model.DTO;
using Client.MVVM.Model.Utilities;
using Client.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MVVM.Model
{
    public class Router
    {
        private readonly UserStore userStore;
        private readonly TravelService travelService;
        public Router(UserStore _userStore, TravelService _travelService)
        {
            userStore = _userStore;
            travelService = _travelService;
        }

        public async Task GoToCurrentArea()
        {
            TravelDTO dto = new()
            {
                CharacterName = userStore.CurrentUser.CharacterName,
                Area = userStore.CurrentUser.CurrentArea
            };
            var response = await travelService.GetCurrentPage<APIResponse>(dto);

            if (response != null && response.IsSuccess)
            {
                var result = JsonConvert.DeserializeObject<TravelDTO>(Convert.ToString(response.Result));
                Area area = result.Area;

                await Shell.Current.GoToAsync(area.ToString());
            }
        }

        public async Task GoToNewArea(Area area)
        {
            TravelDTO dto = new()
            {
                CharacterName = userStore.CurrentUser.CharacterName,
                Area = area
            };

            var response = await travelService.PushNewPage<APIResponse>(dto);
            if (response != null && response.IsSuccess)
            {
                var result = JsonConvert.DeserializeObject<TravelDTO>(Convert.ToString(response.Result));
                userStore.CurrentUser.CurrentArea = area;

                if (result.Area == area)
                { 
                    await Shell.Current.GoToAsync(area.ToString());
                }
            }
        }
    }
}
