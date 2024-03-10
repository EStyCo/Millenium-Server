using Client.MVVM.Model.DTO;
using Microsoft.AspNetCore.SignalR.Client;
using PropertyChanged;

namespace Client.Services
{
    [AddINotifyPropertyChangedInterface]
    public class SpellService
    {
        public List<SpellDTO> SpellList { get; set; }

        public SpellService()
        {
            SpellList = new();
        }
    }
}
