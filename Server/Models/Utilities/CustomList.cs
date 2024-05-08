namespace Server.Models.Utilities
{
    public class CustomList<T>
    {
        public List<T> list { get; set; }

        public CustomList(List<T> _list)
        {
            list = _list;
        }
    }
}
