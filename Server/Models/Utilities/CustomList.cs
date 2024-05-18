namespace Server.Models.Utilities
{
    public class CustomList<T>
    {
        public List<T> list { get; set; } = new();

        public CustomList(IEnumerable<T> _list)
        {
            list.AddRange(_list.ToList());
        }
    }
}
