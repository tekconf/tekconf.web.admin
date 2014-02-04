using System;

namespace TekConf.Web.Admin.ViewModels.Conference
{
    public class ConferenceCreatedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

    }
}