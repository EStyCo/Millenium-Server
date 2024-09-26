﻿namespace Server.Models.Modifiers.Additional
{
    public class AdditionalStrength : Modifier
    {
        public override string Name { get; } = "Дополнительная сила.";
        public override int Value { get; set; } = 0;
        public override int OriginalValue { get; set; } = 0;
        public override string Description { get; } = "";
    }
}
