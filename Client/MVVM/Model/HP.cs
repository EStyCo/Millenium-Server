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
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public int ManaPoint { get; set; }

        public async Task SetHP(int[] newHP)
        {
            CurrentHP = newHP[0];
            MaxHP = newHP[1];
        }

        public async Task SetMP(int mp)
        {
            ManaPoint = mp;
        }
    }
}
