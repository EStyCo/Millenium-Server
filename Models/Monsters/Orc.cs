namespace Server.Models.Monsters
{
    public class Orc : Monster
    {
        public Orc()
        {
            CurrentHP = 97;
            MaxHP = 97;
            Name = "Orc " + GetRandomName();
            ImagePath = "orc_image.png";
        }

        public override int Attack()
        {
            return 5;
        }

        private string GetRandomName()
        {
            string[] array = ["Serega", "Alex", "Grigor", "Maxim", "Artem", "Anton"];

            return array[new Random().Next(0, array.Length)];
        }
    }
}
