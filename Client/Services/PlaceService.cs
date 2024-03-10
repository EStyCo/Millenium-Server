using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.ViewModel;
using Microsoft.AspNetCore.SignalR.Client;
using PropertyChanged;
using System.Windows.Input;

namespace Client.Services
{
    [AddINotifyPropertyChangedInterface]
    public class PlaceService
    {
        private readonly string baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5266" : "https://localhost:7082";
        private HubConnection connection;
        private readonly GladeViewModel viewModel;
        private readonly UserStore userStore;
        private readonly MonsterService monsterService;

        private int targetId = 999;
        public List<Monster> Monsters { get; set; }

        public ICommand AttackTargetCommand { get; set; }
        public ICommand SelectMonsterCommand { get; set; }

        public PlaceService(GladeViewModel _viewModel,
                            UserStore _userStore,
                            MonsterService _monsterService)
        {
            viewModel = _viewModel;
            userStore = _userStore;
            monsterService = _monsterService;

            AttackTargetCommand = new Command<int>(async (spellId) => await Attack(spellId));
            SelectMonsterCommand = new Command<int>(async (id) => await SelectMonster(id));
        }

        public async Task ConnectToHub()
        {
            connection = new HubConnectionBuilder()
                .WithUrl($"{baseUrl}/GladeHub")
                .Build();

            connection.On<List<MonsterDTO>>("UpdateList", (List<MonsterDTO> mDTOList) =>
            {
                Monsters = new();

                foreach (MonsterDTO mDTO in mDTOList)
                {
                    Monster m = new();
                    m.Id = mDTO.Id;
                    m.CurrentHP = mDTO.CurrentHP;
                    m.MaxHP = mDTO.MaxHP;
                    m.Name = mDTO.Name;
                    m.ImagePath = mDTO.ImagePath;
                    Monsters.Add(m);
                }
                SelectMonster();
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

        private async Task Attack(int spellId)
        {
            AttackMonsterDTO attack = new();
            attack.IdMonster = targetId;
            attack.NameCharacter = userStore.Character.CharacterName;
            attack.SkillId = spellId;

            await monsterService.AttackMonster<APIResponse>(attack);
        }

        private async Task SelectMonster(int id)
        {
            foreach (var item in Monsters)
            {
                item.IsTarget = false;
            }

            var monster = Monsters.FirstOrDefault(x => x.Id == id);

            if (monster != null)
            {
                monster.IsTarget = true;
                targetId = id;
            }
        }

        public void SelectMonster()
        {
            var monster = Monsters.FirstOrDefault(x => x.Id == targetId);

            if (monster != null)
            {
                monster.IsTarget = true;
            }
        }
    }
}
