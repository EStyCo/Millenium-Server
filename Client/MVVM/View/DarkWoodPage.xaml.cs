using Client.MVVM.ViewModel;

namespace Client.MVVM.View;

public partial class DarkWoodPage : ContentPage
{
	public DarkWoodPage(DarkWoodViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}