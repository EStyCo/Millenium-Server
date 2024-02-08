using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MVVM.Model
{
    public class User
    {
        public int Id { get; set; }
        public string CharacterName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Race { get; set; }
        public int Level { get; set; }
        public Location CurrentLocation { get; set; }
    }
}
