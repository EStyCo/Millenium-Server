using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.Model.Utilities;
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
        private string baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://147.45.75.109:5000" : "http://147.45.75.109:5000";
        public Character Character { get; set; }
        public SpellService SpellService { get; set; }
        public VitalityService VitalityService { get; set; }


        private HubConnection connection;

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

            connection.On("UpdateHP", (int[] newHP) =>
            {
                VitalityService.CurrentHP = newHP[0];
                VitalityService.MaxHP = newHP[1];
            });

            connection.On("UpdateMP", (int[] newMP) =>
            {
                VitalityService.CurrentMP = newMP[0];
                VitalityService.MaxMP = newMP[1];
            });

            connection.On("UpdateSpellList", (List<SpellDTO> newList) =>
            {
                SpellService.SpellList = newList;
            });

            connection.On<SpellDTO>("UpdateSpell", UpdateSpell);

            try
            {
                await connection.StartAsync();
                //await connection.InvokeAsync("ConnectHub", Character.CharacterName);
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
