using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.MVVM.ViewModel.CharacterModal
{
    public class CharacterViewModel : IDisposable
    {
        private readonly Router router;
        public UserStore UserStore { get; set; }
        public ICommand ReturnCommand { get; set; }

        public CharacterViewModel(Router _router, UserStore _userStore)
        {
            router = _router;
            router.canBack = true;
            ReturnCommand = new Command(async () => await Return());
            UserStore = _userStore;

            //Shell.Current.SetValue(AppShell.ShowTabsProperty, false);
        }

        private async Task Return()
        {
            await router.GoCurrentArea();
        }

        public void Dispose()
        {
            router.canBack = false;
        }
    }
}
