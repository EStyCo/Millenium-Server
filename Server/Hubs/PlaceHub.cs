using Server.Hubs.Locations.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Server.Hubs.DTO;
using Server.Services;
using Server.Hubs.Locations;

namespace Server.Hubs
{
    public class PlaceHub(
        PlaceService placeService, 
        UserStorage userStorage) : Hub
    {
        public async Task ConnectToHub(ConnectToPlaceHubDTO dto)
        {
            var place = placeService.GetPlace(dto.Place);
            var user = userStorage.ActiveUsers
                                  .Where(x => x.Name == dto.Name)
                                  .FirstOrDefault();

            if (place == null || user == null)
            {
                await OnDisconnectedAsync(new());
                return;
            }
            place.EnterPlace(user, Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var place = placeService.GetPlaceById(Context.ConnectionId);

            if (place != null)
            {
                place.LeavePlace(Context.ConnectionId);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public DescriptionPlace? UpdateDescription(string namePlace)
        {
            var place = placeService.GetPlace(namePlace) as IPlaceInfo;

            if (place != null)
                return new(place.ImagePath, place.Description, place.CanAttackUser);

            return null;
        }

        public IRoute[]? UpdateRoutes(string namePlace)
        {
            var place = placeService.GetPlace(namePlace) as IPlaceInfo;
            return place?.Routes;
        }
    }
}

