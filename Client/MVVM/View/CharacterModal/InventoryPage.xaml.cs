using Client.MVVM.ViewModel.CharacterModal;

namespace Client.MVVM.View.CharacterModal;

public partial class InventoryPage : ContentPage
{
	public InventoryPage(InventoryViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;
    }
}