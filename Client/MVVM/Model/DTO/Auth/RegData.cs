using Client.MVVM.Model.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Client.MVVM.Model.DTO.Auth
{
    public class RegData
    {
        [Required(ErrorMessage = "Введите Имя Персонажа!")]
        public string CharacterName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введите эл. почту")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введите расу")]
        public Race Race { get; set; } = Race.Human;
    }
}
