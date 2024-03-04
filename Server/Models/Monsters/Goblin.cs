namespace Server.Models.Monsters
{
    public class Goblin : Monster
    {
        public Goblin()
        {
            CurrentHP = 25;
            MaxHP = 25;
            Name = "Goblin " + GetRandomName();
            ImagePath = "goblin_image.png";
        }

        public override int Attack()
        {
            return 5;
        }

        private string GetRandomName()
        {
            string[] array = ["Chopa", "Oleg", "Denny", "Said", "Danik", "Vanya"];
            
            return array[new Random().Next(0, array.Length)];
        }
    }
}
