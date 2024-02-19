using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MVVM.Model
{
    [AddINotifyPropertyChangedInterface]
    public class HP
    {
        public int HealthPoint { get; set; }
        public int ManaPoint { get; set; }

        public async Task SetHP(int hp)
        { 
            HealthPoint = hp;
        }

        public async Task SetMP(int mp)
        {
            ManaPoint = mp;
        }
    }
}
