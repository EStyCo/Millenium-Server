using Client.MVVM.Model.DTO;
using Client.MVVM.View;
using Client.MVVM.ViewModel;
using Client.Services.IServices;

namespace Client
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }
    }
}
