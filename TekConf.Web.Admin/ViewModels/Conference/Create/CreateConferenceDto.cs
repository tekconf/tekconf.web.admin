using System;
using System.ComponentModel.DataAnnotations;
using TekConf.Web.Admin.Code;

namespace TekConf.Web.Admin.ViewModels
{
    [ConferenceEndDateValidator]
    public class CreateConferenceDto
    {
        [Required]
        [CreateConferenceNameValidator]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
				public string SquareImageUrl { get; set; }
        public string Tagline { get; set; }
        
        [Display(Name = "Is Live?")]
        public bool IsLive { get; set; }

        [Display(Name = "Is hosted online?")]
        public bool IsOnline { get; set; }
        public bool WillProvideVideos { get; set; }
        
        [Display(Name = "Default talk length (minutes)")]
        public int? DefaultTalkLength { get; set; }

        [Required]
        public DateTime? Start { get; set; }
        [Required]
        public DateTime? End { get; set; }

        [Display(Name = "Call for speakers open date")]
        public DateTime? CallForSpeakersOpens { get; set; }
        
        [Display(Name = "Call for speakers close date")]
        public DateTime? CallForSpeakersCloses { get; set; }

        [Display(Name = "Registration open date")]
        public DateTime? RegistrationOpens { get; set; }

        [Display(Name = "Registration close date")]
        public DateTime? RegistrationCloses { get; set; }
    }
}