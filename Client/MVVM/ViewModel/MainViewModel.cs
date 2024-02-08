using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.View;
using Client.Services;
using Client.Services.IServices;
using Newtonsoft.Json;
using PropertyChanged;
using System.Windows.Input;

namespace Client.MVVM.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        private readonly IAuthService authService;
        private readonly UserStore userStore;
        private readonly TravelService travelService;
        private readonly RegistrationPage registrationPage;
        public LoginRequestDTO UserLogin { get; set; }
        public bool isLoading { get; set; } = false;
        public bool canWriting { get; set; } = true;
        public string Response { get; set; } = string.Empty;
        public ICommand LoginCommand { get; set; }
        public ICommand GoToRegisterCommand {  get; set; }

        public MainViewModel(IAuthService _authService, RegistrationPage _registrationPage, UserStore _userStore, TravelService _travelService)
        {
            authService = _authService;
            registrationPage = _registrationPage;
            userStore = _userStore;
            travelService = _travelService;
            UserLogin = new();

            LoginCommand = new Command(async () => await Login());
            GoToRegisterCommand = new Command(async () => await PushRegisterPage());
        }

        private async Task Login()
        {
            isLoading = true;
            canWriting = false;

            var response = await authService.LoginAsync<APIResponse>(UserLogin);
            await Task.Delay(1000);

            if (response != null && response.IsSuccess)
            {
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result));

                userStore.CurrentUser = new()
                {
                    CharacterName = loginResponse.User.CharacterName,
                    Race = loginResponse.User.Race,
                    Level = loginResponse.User.Level,
                    CurrentLocation = loginResponse.User.CurrentLocation
                };

                await travelService.GoToLocationPage();
            }
            else
            {
                Response = response.ErrorMessages.FirstOrDefault().ToString();
                isLoading = false;
                canWriting = true;
            }
        }

        private async Task PushRegisterPage()
        {
            var Navigation = Application.Current.MainPage.Navigation;
            await Navigation.PushAsync(registrationPage);

            //Shell.Current.GoToAsync(nameof(RegistrationPage));
        }
    }
}
