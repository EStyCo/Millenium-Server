﻿using PropertyChanged;

namespace Client.MVVM.Model.DTO
{
    [AddINotifyPropertyChangedInterface]
    public class SpellDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CoolDown { get; set; }
        public bool IsReady { get; set; } = false;
    }
}