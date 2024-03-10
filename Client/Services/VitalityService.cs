using Microsoft.AspNetCore.SignalR.Client;
using PropertyChanged;

namespace Client.Services
{
    [AddINotifyPropertyChangedInterface]
    public class VitalityService
    {
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public int ManaPoint { get; set; }
    }
}
