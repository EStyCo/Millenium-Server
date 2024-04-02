using Client.MVVM.Model.Utilities;
using Client.MVVM.View;
using Client.MVVM.View.CharacterModal;
using Client.MVVM.View.Town;
using Client.MVVM.ViewModel.Town;
using Client.Services;
using Microsoft.Maui.Controls.Xaml;

namespace Client
{
    public partial class AppShell : Shell
    {

        public AppShell()
        {
            InitializeComponent();

            //ShowTabs = false; 
            BindingContext = this;

            Routing.RegisterRoute(Place.Town.ToString(), typeof(TownPage));
            Routing.RegisterRoute(Place.Glade.ToString(), typeof(GladePage));
            Routing.RegisterRoute(Place.SpellMaster.ToString(), typeof(SpellMasterPage));
            Routing.RegisterRoute(Place.DarkWood.ToString(), typeof(DarkWoodPage));

            Routing.RegisterRoute(ModalArea.Character.ToString(), typeof(CharacterPage));
            Routing.RegisterRoute(ModalArea.SpellBook.ToString(), typeof(SpellBookPage));
            Routing.RegisterRoute(ModalArea.Inventory.ToString(), typeof(InventoryPage));

            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(ChatPage), typeof(ChatPage));
        }

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);

            var currentPage = Shell.Current?.CurrentPage;
            if (args.Source == ShellNavigationSource.Pop)
            {
                var backButtonBehavior = currentPage?.FindByName<BackButtonBehavior>("BackButton");

                if (backButtonBehavior != null && !backButtonBehavior.IsVisible && !backButtonBehavior.IsEnabled)
                {
                    args.Cancel();
                }
            }
        }
    }
}
