namespace TekConf.Data.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Capacity { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }

        public virtual Location Location { get; set; }
    }
}