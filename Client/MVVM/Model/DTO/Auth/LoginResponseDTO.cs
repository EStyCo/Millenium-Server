using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client;
using Client.MVVM;
using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.Model.DTO.Auth;

namespace Client.MVVM.Model.DTO.Auth
{
    public class LoginResponseDTO
    {
        public CharacterDTO Character { get; set; }
    }
}
