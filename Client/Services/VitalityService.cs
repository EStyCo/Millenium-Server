using Client.MVVM.Model;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class VitalityService
    {
        private HubConnection connection;
        private readonly HP hp;
        private readonly UserStore userStore;

        public string baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5266" : "https://localhost:7082";
        public VitalityService(HP _hp, UserStore _userStore)
        {
            hp = _hp;
            userStore = _userStore;
        }

        public async Task ConnectionToHub()
        {
            connection = new HubConnectionBuilder()
                .WithUrl($"{baseUrl}/UserStorage")
                .Build();

            connection.On<int[]>("UpdateHP", async (int[] newHP) =>
            {
                await hp.SetHP(newHP);
            });
            connection.On<int>("UpdateMP", async (int newMP) =>
            {
                await hp.SetMP(newMP);
            });

            try
            {
                await connection.StartAsync();
                await connection.InvokeAsync("ConnectHub", userStore.Character.CharacterName);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("",$"Ошибка подключения\n{ex}","Оке");
                return;
            }

        }
    }
}
