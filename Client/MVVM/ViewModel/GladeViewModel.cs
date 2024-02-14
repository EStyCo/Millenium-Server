using Client.MVVM.Model.Utilities;
using Client.MVVM.View.Town;
using Client.Services;
using PropertyChanged;
using System.Windows.Input;

namespace Client.MVVM.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class GladeViewModel
    {
        private readonly UserStore userStore;
        private readonly Router router;
        public ICommand GoToTownCommand { get; set; }

        public GladeViewModel(UserStore _userStore, Router _router)
        {
            userStore = _userStore;
            GoToTownCommand = new Command(async () => await GoToTown());
            router = _router;
        }

        private async Task GoToTown()
        {
            await router.GoToNewArea(Area.Town);
            //await Shell.Current.GoToAsync(Area.Town.ToString());
        }
    }
}
