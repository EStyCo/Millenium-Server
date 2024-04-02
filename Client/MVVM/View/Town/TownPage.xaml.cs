using Client.MVVM.ViewModel.Town;

namespace Client.MVVM.View.Town;

public partial class TownPage : ContentPage
{
	public TownPage(TownViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}