using Client.MVVM.Model.Utilities;
using Client.Services;

namespace Client
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(mainPage);

            MainPage = new AppShell();
        }
    }
}
