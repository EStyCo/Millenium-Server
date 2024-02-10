using Client.MVVM.Model.Utilities;

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
