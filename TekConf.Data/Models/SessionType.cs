namespace TekConf.Data.Models
{
    public class SessionType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Conference Conference { get; set; }
    }
}