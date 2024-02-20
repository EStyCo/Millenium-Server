using Client.MVVM.Model;
using Client.MVVM.ViewModel;

namespace Client.MVVM.View;

public partial class ChatPage : ContentPage
{
	public ChatPage(ChatViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

}