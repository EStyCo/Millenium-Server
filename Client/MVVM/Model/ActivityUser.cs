using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MVVM.Model
{
    public class ActivityUser
    {
        public string CharacterName { get; set; }
        public string Race { get; set; }
        public int Level { get; set; }
        public Location CurrentLocation { get; set; }
    }
}
