using Client.MVVM.View;
using System.Data;
using Location = Client.MVVM.Model.Location;

namespace Client.Services
{
    public class TravelService
    {
        private readonly UserStore userStore;
        public TravelService(UserStore _userStore)
        {
            userStore = _userStore;
        }

        public async Task GoToLocationPage()
        {
            var location = userStore.CurrentUser.CurrentLocation;
            var navigation = Application.Current?.MainPage?.Navigation;

            switch (location)
            {
                case Location.Town:
                    navigation?.InsertPageBefore(new TownPage(), navigation.NavigationStack[0]);
                    break;
                case Location.Glade:
                    navigation?.InsertPageBefore(new GladePage(), navigation.NavigationStack[0]);
                    break;
                case Location.Battle:
                    navigation?.InsertPageBefore(new NewPage1(), navigation.NavigationStack[0]);
                    break;

                default:
                    navigation?.InsertPageBefore(new TownPage(), navigation.NavigationStack[0]);
                    break;
            }

            await navigation.PopToRootAsync();
        }

        /*public async Task GoToLocationPage()
        {
            var location = userStore.CurrentUser.CurrentLocation;
            Application.Current.MainPage = new AppShell();
            string route = string.Empty;

            switch (location)
            {
                case Location.Town:
                    await Shell.Current.GoToAsync(nameof(TownPage));
                    break;
                case Location.Glade:
                    await Shell.Current.GoToAsync(nameof(GladePage));
                    break;
                case Location.Battle:
                    await Shell.Current.GoToAsync(nameof(NewPage1));
                    break;

                default:
                    await Shell.Current.GoToAsync(nameof(TownPage));
                    break;
            }


            await Shell.Current.GoToAsync(route);
        }*/

    }
}