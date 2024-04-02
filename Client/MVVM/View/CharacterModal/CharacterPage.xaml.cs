using Client.MVVM.ViewModel.CharacterModal;

namespace Client.MVVM.View.CharacterModal;

public partial class CharacterPage : ContentPage
{
	public CharacterPage(CharacterViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
    }
}