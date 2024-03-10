using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.View;
using Client.MVVM.View.Town;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using PropertyChanged;

namespace Client.Services
{
    [AddINotifyPropertyChangedInterface]
    public class UserStore
    {
        
        public Character Character { get; set; }
        public SpellService SpellService { get; set; }
        public VitalityService VitalityService { get; set; }


        private HubConnection connection;

        public string baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5266" : "https://localhost:7082";
        public UserStore()
        {
            VitalityService = new();
            SpellService = new();
        }

        public async Task ConnectionHub()
        {
            connection = new HubConnectionBuilder()
                .WithUrl($"{baseUrl}/UserStorage")
                .Build();

            connection.On<int[]>("UpdateHP", (int[] newHP) =>
            {
                VitalityService.CurrentHP = newHP[0];
                VitalityService.MaxHP = newHP[1];
            });
            connection.On<int>("UpdateMP", (int newMP) =>
            {
                VitalityService.ManaPoint = newMP;
            });
            connection.On<List<SpellDTO>>("UpdateSpellList", (List<SpellDTO> newList) =>
            {
                SpellService.SpellList = newList;
            });

            connection.On<SpellDTO>("UpdateSpell", UpdateSpell);

            try
            {
                await connection.StartAsync();
                await connection.InvokeAsync("ConnectHub", Character.CharacterName);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("", $"Ошибка подключения\n{ex}", "Оке");
                return;
            }
        }

        private void UpdateSpell(SpellDTO spellDTO)
        {
            var spell = SpellService.SpellList.FirstOrDefault(x => x.Id == spellDTO.Id);
            
            if (spell != null) 
            { 
                spell.IsReady = spellDTO.IsReady;
                spell.CoolDown = spellDTO.CoolDown;
            }
        }
    }
}
