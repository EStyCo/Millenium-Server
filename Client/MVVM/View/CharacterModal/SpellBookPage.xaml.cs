using Client.MVVM.ViewModel.CharacterModal;

namespace Client.MVVM.View.CharacterModal;

public partial class SpellBookPage : ContentPage
{
	public SpellBookPage(SpellBookViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}