using AutoMapper;
using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.Model.DTO.Auth;
using Client.MVVM.Model.Utilities;
using Client.MVVM.View;
using Client.MVVM.View.Town;
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
        private readonly IMapper mapper;
        private readonly UserStore userStore;
        private readonly TestService testService;
        private readonly Router router;
        public LoginRequestDTO UserLogin { get; set; }
        public bool IsVPS { get; set; } = false;
        public bool isLoading { get; set; } = false;
        public bool canWriting { get; set; } = true;
        public string Response { get; set; } = string.Empty;
        public ICommand LoginCommand { get; set; }
        public ICommand GoToRegisterCommand { get; set; }

        public ICommand TestCommand { get; set; }

        public MainViewModel(IAuthService _authService,
                             IMapper _mapper,
                             RegistrationPage _registrationPage,
                             UserStore _userStore,
                             Router _router,
                             TestService _testService)
        {
            authService = _authService;
            mapper = _mapper;
            router = _router;
            userStore = _userStore;
            UserLogin = new();

            LoginCommand = new Command(async () => await Login());
            GoToRegisterCommand = new Command(async () => await PushRegisterPage());
            TestCommand = new Command(async () => await Test());
            testService = _testService;
        }

        private async Task Login()
        {
            isLoading = true;
            canWriting = false;

            var response = await authService.LoginAsync<APIResponse>(UserLogin);
            //await Task.Delay(1000);

            if (response != null && response.IsSuccess)
            {
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result));
                userStore.Character = mapper.Map<Character>(loginResponse.Character);

                await router.GoCurrentArea();
                await userStore.ConnectionHub();
            }
            else
            {
                Response = response.ErrorMessages.FirstOrDefault().ToString();
            }

            isLoading = false;
            canWriting = true;
        }

        private async Task PushRegisterPage()
        {
            await Shell.Current.GoToAsync(nameof(RegistrationPage));
        }

        private async Task Test()
        {
            var response = await testService.Test<string>();

            await Shell.Current.DisplayAlert("",$"{response}","oke");
        }
    }
}
