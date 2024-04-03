using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MVVM.Model.DTO.Auth
{
    public class RegResponseDTO
    {
        public string Name { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } = false;
    }
}
