namespace TekConf.Data.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Conference Conference { get; set; }
        public virtual Session Session { get; set; }
    }
}