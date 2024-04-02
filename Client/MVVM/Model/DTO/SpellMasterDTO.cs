using Client.MVVM.Model.Utilities;
using PropertyChanged;

namespace Client.MVVM.Model.DTO
{
    [AddINotifyPropertyChangedInterface]
    public class SpellMasterDTO
    {
        public SkillType SkillType {  get; set; }
        public string Name { get; set; } = string.Empty;
        public int CoolDown { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public bool IsLearning { get; set; } = false;
    }
}
