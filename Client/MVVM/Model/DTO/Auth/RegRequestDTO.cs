using Client;
using Client.MVVM;
using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.Model.DTO.Auth;
using Client.MVVM.Model.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Client.MVVM.Model.DTO.Auth
{
    public class RegRequestDTO
    {
        [Required(ErrorMessage = "Введите Имя Персонажа!")]
        public string CharacterName { get; set; }
        [Required(ErrorMessage = "Введите эл. почту")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Введите рассу")]
        public Race Race { get; set; }
    }
}
