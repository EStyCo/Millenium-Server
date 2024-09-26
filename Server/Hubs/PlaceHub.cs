using Microsoft.AspNetCore.SignalR;
using Server.Hubs.DTO;
using Server.Hubs.Locations.Interfaces;
using Server.Models.Interfaces;
using Server.Services;

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
                Console.WriteLine($"Игрок: {dto.Name} не смог подключился к {dto.Place} || ConnectionId: {Context.ConnectionId}");
                await OnDisconnectedAsync(new());
                return;
            }

            place.EnterPlace(user, Context.ConnectionId);

            Console.WriteLine($"Игрок: {dto.Name} подключился к {dto.Place} || ConnectionId: {Context.ConnectionId}");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var place = placeService.GetPlaceById(Context.ConnectionId);

            if (place != null)
            {
                place.LeavePlace(Context.ConnectionId);
            }

            Console.WriteLine($"ConnectionId: {Context.ConnectionId} отключился от {place}");
            await base.OnDisconnectedAsync(exception);
        }

        public DescriptionPlace? UpdateDescription(string namePlace)
        {
            var place = placeService.GetPlace(namePlace) as IPlaceInfo;

            if (place != null)
                return new(place.ImagePath, place.Description, place.CanAttackUser);

            return null;
        }

        public string[]? UpdateRoutes(string namePlace)
        {
            var place = placeService.GetPlace(namePlace) as IPlaceInfo;
            return place?.Routes;
        }
    }
}

