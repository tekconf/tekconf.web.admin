using System;

namespace TekConf.Web.Admin.ViewModels.Conference
{
    public class CreateConferenceDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Tagline { get; set; }
        public bool IsLive { get; set; }
        public bool? IsOnline { get; set; }
        public bool? WillProvideVideos { get; set; }
        public int? DefaultTalkLength { get; set; }
        public DateTime? DatePublished { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public DateTime? CallForSpeakersOpens { get; set; }
        public DateTime? CallForSpeakersCloses { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}