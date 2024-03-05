using Client.MVVM.Model;
using Client.MVVM.ViewModel;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class PlaceService
    {
        private HubConnection connection;
        private readonly GladeViewModel viewModel;

        public string baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5266" : "https://localhost:7082";

        public PlaceService(GladeViewModel _viewModel)
        {
            viewModel = _viewModel;
        }

        public async Task ConnectToHub()
        {
            connection = new HubConnectionBuilder()
                .WithUrl($"{baseUrl}/GladeHub")
                .Build();

            connection.On<List<Monster>>("UpdateList", async (List<Monster> newList) =>
            {
                viewModel.Monsters = newList;
            });

            try
            {
                await connection.StartAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("", $"Ошибка подключения\n{ex.Message.FirstOrDefault()}", "Оке");
                return;
            }
        }

        public async Task Disconnect()
        {
            await connection.StopAsync();
        }
    }
}
