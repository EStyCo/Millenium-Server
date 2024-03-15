using Client.MVVM.ViewModel.Town;

namespace Client.MVVM.View.Town;

public partial class SpellMasterPage : ContentPage
{
	public SpellMasterPage(SpellMasterViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}