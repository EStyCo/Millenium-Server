using Microsoft.AspNetCore.SignalR.Client;
using PropertyChanged;

namespace Client.Services
{
    [AddINotifyPropertyChangedInterface]
    public class VitalityService
    {
        private string hudHP;
        public string HudHP
        {
            get { return $"{CurrentHP}/{MaxHP}"; }
        }
        private string hudMP;
        public string HudMP
        {
            get { return $"{CurrentMP}/{MaxMP}"; }
        }

        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public int CurrentMP { get; set; }
        public int MaxMP { get; set; }
    }
}
