namespace Client
{
    public partial class App : Application
    {
        public App(MainPage mainPage)
        {
            InitializeComponent();

            MainPage = new NavigationPage(mainPage);

            //MainPage = new AppShell();
        }
    }
}
