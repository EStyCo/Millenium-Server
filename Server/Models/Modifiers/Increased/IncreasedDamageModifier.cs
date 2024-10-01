﻿namespace Server.Models.Modifiers.Increased
{
    public class IncreasedDamageModifier : Modifier
    {
        public override string Name { get; } = "Модификатор увеличения урона";
        public override int Value { get; set; } = 0;
        public override int OriginalValue { get; set; } = 0;
        public override string Description { get; } = "";
    }
}