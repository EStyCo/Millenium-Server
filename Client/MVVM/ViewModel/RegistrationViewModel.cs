using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.Services.IServices;
using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.MVVM.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class RegistrationViewModel
    {
        private readonly IAuthService authService;
        public RegistrationRequestDTO UserRegister { get; set; }
        public bool isLoading { get; set; } = false;
        public bool canWriting { get; set; } = true;
        public string Response { get; set; } = string.Empty;
        public ICommand RegisterCommand { get; set; }

        public RegistrationViewModel(IAuthService _authService)
        {
            authService = _authService;
            UserRegister = new();

            RegisterCommand = new Command(async () => await Register());
        }

        private async Task Register()
        { 
            isLoading = true;
            canWriting = false;
            await Task.Delay(1000);

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(UserRegister, serviceProvider: null, items: null);
            var isValid = Validator.TryValidateObject(UserRegister, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                Response = validationResults.FirstOrDefault().ToString();
                isLoading = false;
                canWriting = true;
                return;
            }

            var response = await authService.RegisterAsync<APIResponse>(UserRegister);
            if (response != null && response.IsSuccess)
            {
                Response = response.Result.ToString();
            }
            else
            {
                Response = response.ErrorMessages.FirstOrDefault().ToString();
            }

            isLoading = false;
            canWriting = true;
        }
    }
}
