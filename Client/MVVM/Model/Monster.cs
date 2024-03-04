using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MVVM.Model
{
    [AddINotifyPropertyChangedInterface]
    public class Monster
    {
        public int Id { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }
}
