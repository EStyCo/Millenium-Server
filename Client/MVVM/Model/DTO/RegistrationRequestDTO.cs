using Client.MVVM.Model.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MVVM.Model.DTO
{
    public class RegistrationRequestDTO
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
