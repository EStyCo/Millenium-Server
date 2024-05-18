using Microsoft.AspNetCore.SignalR;
using Server.Hubs.Locations;

namespace Server.Hubs
{
    public class PlaceHub : Hub
    {
        private readonly AreaStorage AreaStorage;
        private readonly UserStorage UserStorage;

        public PlaceHub(AreaStorage areaStorage, 
                        UserStorage userStorage) 
        {
            AreaStorage = areaStorage;
            UserStorage = userStorage;
        }

        public void ConnectToHub(ConnectToPlaceHubDTO dto)
        {
            var place = AreaStorage.GetPlace(dto.Place);
            var stats = UserStorage.ActiveUsers.Where(x => x.Name == dto.Name)
                .Select(x => x.Stats)
                .FirstOrDefault();

            if (place == null || stats == null) return;
            
            place.EnterPlace(dto.Name, stats.Level, Context.ConnectionId);

            Console.WriteLine($"Игрок: {dto.Name} подключился к {dto.Place} || ConnectionId: {Context.ConnectionId}");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var place = AreaStorage.GetPlaceById(Context.ConnectionId);

            if (place != null)
            { 
                place.LeavePlace(Context.ConnectionId);
            }
            
            Console.WriteLine($"ConnectionId: {Context.ConnectionId} отключился от {place}");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
