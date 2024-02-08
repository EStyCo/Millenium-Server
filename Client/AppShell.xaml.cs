using Client.MVVM.View;

namespace Client
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(TownPage), typeof(TownPage));
            Routing.RegisterRoute(nameof(GladePage), typeof(GladePage));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(NewPage1), typeof(NewPage1));
            
        }
    }
}
