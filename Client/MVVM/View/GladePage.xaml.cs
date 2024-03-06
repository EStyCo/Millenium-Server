using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Client.MVVM.ViewModel;

namespace Client.MVVM.View
{
    public partial class GladePage : ContentPage
    {
        public GladePage(GladeViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
