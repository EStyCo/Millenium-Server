using AutoMapper;
using Client.MVVM.Model;
using Client.MVVM.Model.DTO.Auth;
using Client.Services.IServices;
using PropertyChanged;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace Client.MVVM.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class RegistrationViewModel
    {
        private readonly IAuthService authService;
        private readonly IMapper mapper;
        public RegData UserData { get; set; }
        public bool isLoading { get; set; } = false;
        public bool canWriting { get; set; } = true;
        public string Response { get; set; } = string.Empty;
        public ICommand RegisterCommand { get; set; }

        public RegistrationViewModel(IAuthService _authService, IMapper _mapper)
        {
            authService = _authService;
            mapper = _mapper;
            UserData = new();

            RegisterCommand = new Command(async () => await Register());
        }

        private async Task Register()
        { 
            isLoading = true;
            canWriting = false;
            await Task.Delay(1000);

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(UserData, serviceProvider: null, items: null);
            var isValid = Validator.TryValidateObject(UserData, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                Response = validationResults.FirstOrDefault().ToString();
                isLoading = false;
                canWriting = true;
                return;
            }

            var response = await authService.RegisterAsync<APIResponse>(mapper.Map<RegRequestDTO>(UserData));
            if (response != null && response.IsSuccess)
            {
                Response = response.Result.ToString();
            }
            else
            {
                Response = response?.ErrorMessages.FirstOrDefault().ToString();
            }

            isLoading = false;
            canWriting = true;
        }
    }
}
