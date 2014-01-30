namespace TekConf.Data.Models
{
    public class SessionLink
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public virtual Session Session { get; set; }
    }
}