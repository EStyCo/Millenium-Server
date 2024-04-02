using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.MVVM.ViewModel.CharacterModal
{
    public class SpellBookViewModel
    {
        private readonly Router router;
        public ICommand ReturnCommand { get; set; }

        public SpellBookViewModel(Router _router)
        {
            router = _router;
            ReturnCommand = new Command(async () => await Return());

            //Shell.Current.SetValue(AppShell.ShowTabsProperty, false);
        }

        private async Task Return()
        {
            await router.GoCurrentArea();
        }
    }
}
