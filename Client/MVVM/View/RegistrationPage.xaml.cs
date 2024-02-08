using Client.MVVM.Model.DTO;
using Client.MVVM.ViewModel;

namespace Client.MVVM.View;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage(RegistrationViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}