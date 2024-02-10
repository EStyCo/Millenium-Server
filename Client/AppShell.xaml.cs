using Client.MVVM.Model.Utilities;
using Client.MVVM.View;
using Client.MVVM.View.Town;

namespace Client
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(Area.Town.ToString(), typeof(TownPage));
            Routing.RegisterRoute(Area.Glade.ToString(), typeof(GladePage));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(NewPage1), typeof(NewPage1));

        }
        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);

            if (args.Source == ShellNavigationSource.Pop)
            {
                args.Cancel();
            }
        }
    }
}
