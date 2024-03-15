using Client.MVVM.Model;
using Client.MVVM.Model.DTO;
using Client.MVVM.Model.Utilities;
using Client.Services;
using Newtonsoft.Json;
using PropertyChanged;
using System.Windows.Input;

namespace Client.MVVM.ViewModel.Town
{
    [AddINotifyPropertyChangedInterface]
    public class SpellMasterViewModel
    {
        private readonly LearningService learningService;
        private readonly UserStore userStore;

        public Router Router { get; set; }
        public int FreePoints { get; set; }
        public List<SpellMasterDTO> AllSkills { get; set; }

        public ICommand LearnSkillCommand { get; set; }
        public ICommand ForgotSkillCommand { get; set; }


        public SpellMasterViewModel(LearningService _learningService, 
                                    UserStore _userStore,
                                    Router _Router)
        {
            learningService = _learningService;
            userStore = _userStore;
            Router = _Router;

            LearnSkillCommand = new Command<SkillType>(async (skillType) => await LearnSkill(skillType));
            ForgotSkillCommand = new Command<SkillType>(async (skillType) => await ForgotSkill(skillType));


            GetSkill();
        }

        private async Task GetSkill()
        { 
            await Task.Delay(10);
            NameRequestDTO dto = new() { Name = userStore.Character.CharacterName };
            var response = await learningService.GetCharacterSkills<APIResponse>(dto);
            
            if (response != null && response.IsSuccess)
            {
                var result = JsonConvert.DeserializeObject<LearningResponseDTO>(Convert.ToString(response.Result));
                FreePoints = result.FreePoints;
                AllSkills = result.AllSkills;
            }
        }

        private async Task LearnSkill(SkillType skillType)
        {
            await Task.Delay(10);
            LearnSkillDTO dto = new() 
            {
                CharacterName = userStore.Character.CharacterName,
                SkillType = skillType
            };

            await learningService.LearnSkill<APIResponse>(dto);

            await GetSkill();
        }

        private async Task ForgotSkill(SkillType skillType)
        {
            await Task.Delay(10);
            LearnSkillDTO dto = new()
            {
                CharacterName = userStore.Character.CharacterName,
                SkillType = skillType
            };

            await learningService.ForgotSkill<APIResponse>(dto);

            await GetSkill();
        }
    }
}
