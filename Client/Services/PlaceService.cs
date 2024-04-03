using AutoMapper;
using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.Model.Utilities;
using Client.MVVM.ViewModel;
using Microsoft.AspNetCore.SignalR.Client;
using PropertyChanged;
using System.Windows.Input;

namespace Client.Services
{
    [AddINotifyPropertyChangedInterface]
    public class PlaceService
    {
        private readonly string hubUrl = string.Empty;
        private HubConnection connection;
        private readonly IMapper mapper;

        private readonly UserStore userStore;
        private readonly MonsterService monsterService;

        private int targetId = 999;
        public List<Monster> Monsters { get; set; }

        public ICommand AttackTargetCommand { get; set; }
        public ICommand SelectMonsterCommand { get; set; }

        public PlaceService(UserStore _userStore,
                            MonsterService _monsterService,
                            string _hubUrl,
                            IMapper _mapper)
        {
            userStore = _userStore;
            monsterService = _monsterService;
            hubUrl = _hubUrl;
            mapper = _mapper;

            AttackTargetCommand = new Command<int>(async (spellId) => await Attack(spellId));
            SelectMonsterCommand = new Command<int>(async (id) => await SelectMonster(id));
        }

        public async Task ConnectToHub()
        {
            connection = new HubConnectionBuilder()
                .WithUrl($"{BaseUrl.Get()}/{hubUrl}Hub")
                .Build();

            connection.On("UpdateList", (List<MonsterDTO> mList) =>
            {
                Monsters = new();

                foreach (var m in mList)
                {
                    Monsters.Add(mapper.Map<Monster>(m));
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
            await monsterService.AttackMonster<APIResponse>(new()
            {
                IdMonster = targetId,
                NameCharacter = userStore.Character.CharacterName,
                Place = userStore.Character.CurrentArea,
                SkillId = spellId
            });
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
