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
        public UserStore UserStore {  get; set; }
        public ICommand GoToGladeCommand { get; set; }

        public TownViewModel(UserStore _userStore, Router _router)
        {
            UserStore = _userStore;
            GoToGladeCommand = new Command(async () => await GoToGlade());
            router = _router;
        }

        private async Task GoToGlade()
        {

            await router.GoToNewArea(Area.Glade);
            //await Shell.Current.GoToAsync(Area.Glade.ToString());
        }

        private async Task GoNewPage()
        {
            
        }
    }
}
