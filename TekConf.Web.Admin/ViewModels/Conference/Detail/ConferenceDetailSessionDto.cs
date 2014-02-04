using System;

namespace TekConf.Web.Admin.ViewModels.Conference
{
    public class ConferenceDetailSessionDto
    {
        public string Title { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int? TalkLength { get; set; }
        public string Description { get; set; }
        public bool WillProvideVideo { get; set; }
    }
}