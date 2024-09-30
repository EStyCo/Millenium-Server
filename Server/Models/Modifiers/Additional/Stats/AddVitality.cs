﻿namespace Server.Models.Modifiers.Additional.Stats
{
    public class AddVitality : Modifier
    {
        public override string Name { get; } = "Дополнительная выносливость.";
        public override int Value { get; set; } = 0;
        public override int OriginalValue { get; set; } = 0;
        public override string Description { get; } = "";
    }
}
