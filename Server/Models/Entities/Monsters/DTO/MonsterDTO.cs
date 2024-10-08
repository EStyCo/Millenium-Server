﻿using Server.Models.DTO.Inventory;
using Server.Models.DTO.User;

namespace Server.Models.Entities.Monsters.DTO
{
    public class MonsterDTO
    {
        public int Id { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string Target { get; set; } = string.Empty;
        public List<StateDTO> States { get; set; } = new();
    }
}
